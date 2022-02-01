using FileArchiver.Common.Helpers;
using FileArchiver.Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileArchiver.MVC.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CredentialsViewModel loginCredentials)
        {
            HttpContext.Session.SetString("SessionCredentials", JsonConvert.SerializeObject(loginCredentials));
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getUserMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:login");
            UserViewModel user;
            try
            {
                user = await HttpClientHelper.GetUser(credentials, baseAddress + getUserMethod);
            }
            catch (System.Exception)
            {
                ViewBag.ErrorMessage = "The username or password was wrong try again";
                return View("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!user.IsAdmin)
            {
                return RedirectToAction("GetToUserView", "Home", user);
            }
            return RedirectToAction("GetToAdminView", "Home", user);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
