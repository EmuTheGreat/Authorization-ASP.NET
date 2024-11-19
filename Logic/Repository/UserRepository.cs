using Logic.Models;
using Logic.Models.Interfaces;
using Logic.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Logic.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetByPhone(string phone)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Phone == phone);
        }

        public async Task UpdateLastLoginTime(User user)
        {
            var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (userToUpdate is not null)
            {
                userToUpdate.LastLogin = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
