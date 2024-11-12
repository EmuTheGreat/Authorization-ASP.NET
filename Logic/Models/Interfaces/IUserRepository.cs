namespace Logic.Models.Interfaces
{
    public interface IUserRepository
    {
        public void Create(User user);
        public void UpdateLastLoginTime(User user);
        public IEnumerable<User> GetAll();
        public User GetByEmail(string email);
        public User GetByPhone(string phone);
    }
}
