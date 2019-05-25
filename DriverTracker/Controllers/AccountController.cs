using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using DriverTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using DriverTracker.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    /// <summary>
    /// Controller class for Web API authentication
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // POST: api/account/maketoken
        [HttpPost("maketoken")]
        public async Task<IActionResult> MakeToken([FromBody] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync(login.Input.Email, login.Input.Password, false, false);

                if (!loginResult.Succeeded) { return BadRequest(); }
                IdentityUser user = await _userManager.FindByEmailAsync(login.Input.Email);

                return Ok(await GetToken(user));
            }
            return BadRequest(ModelState);
        }

        // POST: api/account/makesessionusertoken
        [HttpPost("makesessionusertoken")]
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public async Task<IActionResult> MakeSessionUserToken()
        {
            IdentityUser user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            return Ok(await GetToken(user));
        }

        // POST: api/account/refreshtoken
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            IdentityUser user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)
            ?? User.Claims.Where(c => c.Properties.ContainsKey(JwtRegisteredClaimNames.UniqueName))
                .Select(c => c.Value).FirstOrDefault());
            return Ok(await GetToken(user));
        }

        private async Task<string> GetToken(IdentityUser user)
        {
            var utcNow = DateTime.UtcNow;

            // generate a new token
            string jti = Guid.NewGuid().ToString();

            Claim[] claims = {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            ClaimsIdentity identity = new ClaimsIdentity(claims, "Token");
            identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("APITokens:Key")));
            SigningCredentials signingCredentials = new SigningCredentials(
                signingKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: identity.Claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("APITokens:Lifetime")),
                audience: _configuration.GetValue<string>("APITokens:Audience"),
                issuer: _configuration.GetValue<string>("APITokens:Issuer")
            );
                    
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
