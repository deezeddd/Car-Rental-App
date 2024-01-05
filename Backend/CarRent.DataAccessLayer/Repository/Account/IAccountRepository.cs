using CarRent.DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;

namespace CarRent.DataAccessLayer.Repository.Account
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<string> LoginAsync(LoginUserModel signInModel);


    }
}