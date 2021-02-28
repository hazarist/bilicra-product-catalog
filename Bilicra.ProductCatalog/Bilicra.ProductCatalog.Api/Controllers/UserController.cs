using Bilicra.ProductCatalog.Business.Interfaces;
using Bilicra.ProductCatalog.Common.Models.AuthenticationModels;
using Bilicra.ProductCatalog.Common.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication(AuthenticationPostModel request)
        {
            var serviceRepsonse = await userService.AuthenticateAsync(request.Username, request.Password);
            return Ok(serviceRepsonse);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(UserPostModel request)
        {
            var serviceRepsonse = await  userService.CreateUserAsync(request);
            return Ok(serviceRepsonse);
        }
    }
}
