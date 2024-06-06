using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductDbContext _productDbContext;


        public UserRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _productDbContext.Users.ToListAsync();
        }

        public async Task<User> AddUser(User user)
        {
            await _productDbContext.Users.AddAsync(user);
            await _productDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            return await _productDbContext.Users
                .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
        }

        public async Task<User> UpdateUser(User userToUpdate)
        {
            var user = await _productDbContext.Users.FindAsync(userToUpdate.Userid);
            if (user != null)
            {
                user.FirstName = userToUpdate.FirstName;
                user.LastName = userToUpdate.LastName;
                user.Email = userToUpdate.Email;
                user.Password = userToUpdate.Password;

                _productDbContext.Users.Update(user);  

                await _productDbContext.SaveChangesAsync();
            }
            return user;
        }

        public int CheckPassword(int res)
        {
            return res;
        }
    }
}