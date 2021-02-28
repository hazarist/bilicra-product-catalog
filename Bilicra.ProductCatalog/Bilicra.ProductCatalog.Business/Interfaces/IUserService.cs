using Bilicra.ProductCatalog.Common.Entities;
using Bilicra.ProductCatalog.Common.Models.UserModels;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> AuthenticateAsync(string username, string password);
        Task<UserModel> CreateUserAsync(UserPostModel request);
        Task<bool> IsUserExist(string username);
    }
}
