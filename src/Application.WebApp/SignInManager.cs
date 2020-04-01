using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.WebApp
{
    public class SignInManager : SignInManager<User>
    {
        public SignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new InvalidOperationException("User does not exist");
            }
            
            return await (false ? 
                Task.FromResult(SignInResult.LockedOut) : 
                base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure));
        }
        
        
    }
}