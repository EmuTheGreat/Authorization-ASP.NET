using Logic.Models;
using Logic.Models.Interfaces;
using Logic.DbContext;

namespace Logic.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(User user)
        {
            _dbContext.AddAsync(user);
            _dbContext.SaveChangesAsync();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public User GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public User GetByPhone(string phone)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Phone == phone);
        }

        public void UpdateLastLoginTime(User user)
        {
            var userToUpdate = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userToUpdate is not null) 
            { 
                userToUpdate.LastLogin = DateTime.Now;
                _dbContext.SaveChangesAsync();
            }
        }
    }
}
