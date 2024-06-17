using EncryptStringSample;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorServerLoginWithAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            //if (username != "na1UBAgWNe0UNp4HUw2AOA==" || password != "na1UBAgWNe0UNp4HUw2AOA==")
            //{
            //    return Unauthorized();
            //}

            Claim ageClaim = new Claim("age", "18");
            Claim sexClaim = new Claim("sex", "male");
            Claim roleClaim = new Claim(ClaimTypes.Role, "Admin");
            ClaimsIdentity claimsPersonal = new ClaimsIdentity(new[] { ageClaim, sexClaim, roleClaim }, "Auth With Server 1");

            Claim countryClaim = new Claim(ClaimTypes.Country, "Iran");
            Claim LanguageClaim = new Claim("Language", "fa");
            ClaimsIdentity claimsRegionIdentity = new ClaimsIdentity(new[] { countryClaim, LanguageClaim }, "Auth With Server 2");

            ClaimsPrincipal principal = new ClaimsPrincipal(new[] { claimsPersonal, claimsRegionIdentity });

            await HttpContext.SignInAsync(principal);
            return Redirect("~/");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/loginpage");
        }
    }
}
