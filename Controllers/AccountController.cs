using Auth0.AspNetCore.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrijemniMVC.Controllers
{
    [AllowAnonymous,Route("account")]
    public class AccountController : Controller
    {
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties=new AuthenticationProperties { RedirectUri=Url.Action("GoogleResponse")};
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [Route("google-response")]
        public async Task<ActionResult> GoogleResponse(string returnUrl)
        {
            var result= await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(c => new
                {
                    c.Issuer,
                    c.OriginalIssuer,
                    c.Type,
                    c.Value
                });
            return RedirectToAction("Index", "Students");
        }
       
    }
}
