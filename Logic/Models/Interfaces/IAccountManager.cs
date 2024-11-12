using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;

namespace Logic.Models.Interfaces
{
    public interface IAccountManager
    {
        public Task<BaseResponse<ClaimsIdentity>> RegisterAsync(User user);
        public Task<BaseResponse<ClaimsIdentity>> LoginAsync(User user);
        public User GetUserByEmail(string email);
    }
}
