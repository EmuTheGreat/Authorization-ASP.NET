using AutoMapper;
using Logic.Models.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CabinetController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtProvider _jwtProvider;

        public CabinetController(IAccountManager accountManager, IConfiguration configuration, IJwtProvider jwtProvider)
        {
            _accountManager = accountManager;
            _configuration = configuration;
            _jwtProvider = jwtProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var token = HttpContext.Request.Cookies["tasty-cookies"];
            if (token == null || !_jwtProvider.IsTokenValid(_configuration["JwtOptions:Key"], _configuration["JwtOptions:Issuer"], token))
            {
                return RedirectToAction("Login", "Account");
            }
            var claims = User.Claims.ToList()[1];
            var user = _accountManager.GetUserByEmail(claims.Value);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            Response.Cookies.Delete("tasty-cookies");
            return RedirectToAction("Login", "Account");
        }
    }
}
