namespace Logic.Models.Interfaces
{
    public interface IAccountManager
    {
        public Task<BaseResponse> RegisterAsync(User user);
        public Task<BaseResponse> LoginAsync(User user);
        public Task<User> GetUserByEmail(string email);
    }
}
