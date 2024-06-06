using Entities;
using Repository;
using Zxcvbn;
namespace Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


       public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }
        public async Task<User> AddUser(User user)

        {
            var res = Zxcvbn.Core.EvaluatePassword(user.Password.ToString());
            if (res.Score < 2){
                return null;
            }
            User u = await userRepository.AddUser(user);
                return u;
        }

        public async Task<User> Login(String userName, String password)
        {
            return await userRepository.Login(userName, password);
        }
        public async Task<User> UpdateUser( User updatedUser)
        {
           
            return await userRepository.UpdateUser(updatedUser);
        }
        public int CheckPassword(string pass)
        {
            var res = Zxcvbn.Core.EvaluatePassword(pass.ToString());

            return userRepository.CheckPassword( res.Score);
        }
    }
}
