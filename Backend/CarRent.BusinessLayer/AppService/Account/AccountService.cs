using CarRent.DataAccessLayer.Model;
using CarRent.DataAccessLayer.Repository.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarRent.BusinessLayer.AppService.Account.AccountService;

namespace CarRent.BusinessLayer.AppService.Account
{
    public class AccountService
    {
        public class AccountServices : IAccountServices
        {
            private readonly IAccountRepository _accountRepository;
            public AccountServices(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
            {
                return await _accountRepository.CreateUserAsync(userModel);
            }

            public async Task<String> LoginAsync(LoginUserModel signInModel)
            {
                return await _accountRepository.LoginAsync(signInModel);
            }

        }
    }
}
