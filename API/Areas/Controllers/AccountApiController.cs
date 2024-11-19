using Logic.Models;
using Logic.Models.Interfaces;
using API.Models.DTO.Request;
using API.Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace API.Areas.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountManager _accountManager;
        public AccountApiController(IMapper mapper, IAccountManager accountManager)
        {
            _mapper = mapper;
            _accountManager = accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var user = _mapper.Map<User>(registerRequest);
            var response = await _accountManager.RegisterAsync(user);

            if (response.StatusCode == 200)
            {
                return Ok();
            }

            return BadRequest(new ErrorResponse()
            {
                Code = "400",
                Message = response.Description
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = _mapper.Map<User>(loginRequest);
            var response = await _accountManager.LoginAsync(user);
            
            if(response.StatusCode == 200)
            {
                Response.Cookies.Append("tasty-cookies", response.Token, new CookieOptions
                {
                    HttpOnly = true,   
                    Secure = true,     
                    SameSite = SameSiteMode.Strict
                });
                return Ok();
            }

            return BadRequest(new ErrorResponse()
            {
                Code = "400",
                Message = response.Description
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("tasty-cookies");
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-my-info")]
        public async Task<IActionResult> GetMyInfo()
        {
            var email = User.Claims.ToList()[1].Value;
            var user = await _accountManager.GetUserByEmail(email);
            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }
    }
}


