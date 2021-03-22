using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VueBookLibrary.ServerApp.Models;

namespace VueBookLibrary.ServerApp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountValuesController : ControllerBase
    {
		private IConfiguration configuration;
		private SignInManager<IdentityUser> signinManager;
		private UserManager<IdentityUser> userManager;

		public AccountValuesController(SignInManager<IdentityUser> mgr, UserManager<IdentityUser> usermgr, IConfiguration config)
		{
			signinManager = mgr;
			userManager = usermgr;
			configuration = config;
		}

		[HttpPost]
		public async Task<IActionResult> Login(Credentials creds)
		{
			if (await CheckPassword(creds))
			{
				string token = GenerateToken(creds);
				return Ok(new { success = true, token = token });
			}
			return Unauthorized();
		}

		private async Task<bool> CheckPassword(Credentials creds)
		{
			IdentityUser user = await userManager.FindByEmailAsync(creds.Email);
			if (user != null)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await signinManager.PasswordSignInAsync(user, creds.Password, true, true);
				if (result.Succeeded)
				{
					return true;
				}
			}
			return false;
		}

		private string GenerateToken(Credentials creds)
        {
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
			byte[] secret = Encoding.ASCII.GetBytes(configuration["jwtSecret"]);
			SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Email, creds.Email) }),
				//Expires = DateTime.UtcNow.AddHours(24),	// Time is unlimited
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
			};
			SecurityToken token = handler.CreateToken(descriptor);

			return handler.WriteToken(token);
		}
	}
}
