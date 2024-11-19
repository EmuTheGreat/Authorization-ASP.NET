namespace Logic.Models.Interfaces
{
    public interface IUserRepository
    {
        public Task Create(User user);
        public Task UpdateLastLoginTime(User user);
        public IEnumerable<User> GetAll();
        public Task<User> GetByEmail(string email);
        public Task<User> GetByPhone(string phone);
    }
}
