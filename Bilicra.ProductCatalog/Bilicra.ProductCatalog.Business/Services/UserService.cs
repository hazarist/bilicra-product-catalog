using AutoMapper;
using Bilicra.ProductCatalog.Business.Interfaces;
using Bilicra.ProductCatalog.Common.Entities;
using Bilicra.ProductCatalog.Common.Exceptions;
using Bilicra.ProductCatalog.Common.Models.UserModels;
using Bilicra.ProductCatalog.Common.Settings;
using Bilicra.ProductCatalog.DataAccess.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> userRepository;
        private readonly AppSettings options;
        private readonly IMapper mapper;
        public UserService(IRepository<UserEntity> userRepository, IMapper mapper, IOptions<AppSettings> options)
        {
            this.userRepository = userRepository;
            this.options = options.Value;
            this.mapper = mapper;
        }

        public async Task<UserModel> AuthenticateAsync(string username, string password)
        {

            var userList = await userRepository.GetAllAsync(x => x.Username == username);
            var user = userList.SingleOrDefault();
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var hashedPassword = HashPassword(password, Convert.FromBase64String(user.Salt));

            if (user.Password != hashedPassword)
            {
                throw new BadRequestException("Wrong password");
            }

            user.AccessToken = GenerateToken(user);
            var userResponse = mapper.Map<UserModel>(user);
            return userResponse;
        }

        public async Task<UserModel> CreateUserAsync(UserPostModel request)
        {
            var isUserExist = await IsUserExist(request.Username);
            if (isUserExist)
            {
                throw new BadRequestException("Username allready has taken");
            }

            var salt = GetSalt();
            var hashedPassword = HashPassword(request.Password, salt);

            var user = mapper.Map<UserEntity>(request);
            user.Password = hashedPassword;
            user.Salt = Convert.ToBase64String(salt);

            await userRepository.CreateAsync(user);
            var userResponse = mapper.Map<UserModel>(user);
            return userResponse;
        }

        public string GenerateToken(UserEntity user)
        {
            var expire = DateTime.UtcNow.AddDays(7);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username",user.Username),
                new Claim("Id", user.Id.ToString()),
                new Claim("Expire", expire.Ticks.ToString() ?? string.Empty)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> IsUserExist(string username)
        {
            var users = await userRepository.GetAllAsync();
            return users.Any(x => x.Username == username);
        }

        public byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
