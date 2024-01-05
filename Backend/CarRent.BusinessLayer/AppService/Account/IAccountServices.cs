using CarRent.DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;

namespace CarRent.BusinessLayer.AppService.Account
{
    public interface IAccountServices
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<String> LoginAsync(LoginUserModel signInModel);
    }
}