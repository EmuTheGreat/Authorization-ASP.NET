using AutoMapper;
using Logic.DbContext;
using Logic.Models.Interfaces;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO.Request;

namespace WebApp.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAccountManager _accountManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(ILogger<AccountController> logger,
            IMapper mapper, IAccountManager accountManager, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _accountManager = accountManager;
            _dbContext = dbContext;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
        {
            var user = _mapper.Map<User>(registerRequest);
            var response = await _accountManager.RegisterAsync(user);

            if (response.StatusCode == 200)
            {
                Response.Cookies.Append("tasty-cookies", response.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return RedirectToAction("Index", "Home");
            }

            if (response.PhoneDescription != null)
                ModelState.AddModelError("Phone", response.PhoneDescription);

            if (response.Description != null)
                ModelState.AddModelError("Email", response.Description);
            return View(registerRequest);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
        {
            var user = _mapper.Map<User>(loginRequest);
            var response = await _accountManager.LoginAsync(user);

            if (response.StatusCode == 200)
            {
                Response.Cookies.Append("tasty-cookies", response.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return RedirectToAction("Index", "Cabinet");
            }

            ModelState.AddModelError("Phone", response.Description);
            return View(loginRequest);
        }
    }
}
