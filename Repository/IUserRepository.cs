using Entities;

namespace Repository;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();

    public Task<User> AddUser(User user);
    public Task<User> Login(String userName, String password);
     public Task<User> UpdateUser(User userToUpdate);
    public int CheckPassword(int res);

}