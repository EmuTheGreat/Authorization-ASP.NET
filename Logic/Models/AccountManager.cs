using Logic.Models.Interfaces;
using Logic.Models;
using System.Security.Claims;

namespace Logic.Models
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;

        public AccountManager(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public async  Task<BaseResponse<ClaimsIdentity>> LoginAsync(User user)
        {
            var userByPhone = _userRepository.GetByPhone(user.Phone);
            
            if (userByPhone is null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 400,
                    Description = "Пользователя с таким номером не существует."
                };
            }

            if (!_passwordHasher.VerifyPassword(user.Password, userByPhone.Password))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 400,
                    Description = "Неверный пароль."
                };
            }

            var token = _jwtProvider.GenerateToken(userByPhone,
                _configuration["JwtOptions:Key"],
                _configuration["JwtOptions:Issuer"],
                int.Parse(_configuration["JwtOptions:ExpiresHours"]));

            _userRepository.UpdateLastLoginTime(userByPhone);
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 200,
                Description = "Вы успешно вошгли.",
                Token = token
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterAsync(User user)
        {
            var userByEmail = _userRepository.GetByEmail(user.Email);
            if (userByEmail is not null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 400,
                    Description = "Пользователь с такой почтой уже существует."
                };
            }

            var userByPhone = _userRepository.GetByPhone(user.Phone);
            if (userByPhone is not null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 400,
                    Description = "Пользователь с таким номером уже существует."
                };
            }

            var token = _jwtProvider.GenerateToken(user,
                _configuration["JwtOptions:Key"],
                _configuration["JwtOptions:Issuer"],
                int.Parse(_configuration["JwtOptions:ExpiresHours"]));

            user.Password = _passwordHasher.Generate(user.Password);
            _userRepository.Create(user);
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 200,
                Description = "Регистрация успешна.",
                Token = token
            };
        }
    }
}
