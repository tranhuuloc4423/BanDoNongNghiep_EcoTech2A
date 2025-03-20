using Source_Demo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Source_Demo.Lib
{
    public class M_AccountSecurity
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string Avatar { get; set; }
        public bool StayLoggedIn { get; set; }
    }
    public class SecurityManager
    {
        private IEnumerable<Claim> getUserClaims(M_AccountSecurity account)
        {
            return new List<Claim>()
            {
                new Claim("UserId", account.UserId),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim("Avatar", account.Avatar),
                new Claim("AccessToken", account.AccessToken),
            }; ;
        }
        public async void SignIn(HttpContext httpContext, M_AccountSecurity account, string scheme)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(getUserClaims(account), scheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(
                scheme: scheme, 
                principal: claimsPrincipal,
                properties: new AuthenticationProperties
                {
                    IsPersistent = account.StayLoggedIn,
                    ExpiresUtc = DateTime.Now.AddDays(7)
                });
        }
        public async void SignOut(HttpContext httpContext, string scheme)
        {
            await httpContext.SignOutAsync(scheme);
        }
    }
}
