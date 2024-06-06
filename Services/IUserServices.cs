using Entities;

namespace Services
{
    public interface IUserServices
    {
        public Task<IEnumerable<User>>  GetUsers();
        public Task<User> AddUser(User user);
        public Task<User> Login(String userName, String password);
        public Task<User> UpdateUser(User userToUpdate);
        public int CheckPassword(string pass);

    }
}