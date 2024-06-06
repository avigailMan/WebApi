using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UserRepositoryIntegrationTest : IClassFixture<DataBaseFixture>
    {
        private readonly ProductDbContext _dbContext;
        private readonly UserRepository _userRepository;

        public UserRepositoryIntegrationTest(DataBaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            var email = "test@gmail.com";
            var password = "password";
            var user = new User { Email = email, Password = password, FirstName = "test", LastName = "test22" };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await _userRepository.Login(email, password);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task AddUser_ValidUser_ReturnsAddedUser()
        {
            var user = new User
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "test@gmail.com",
                Password = "password"
            };

            var result = await _userRepository.AddUser(user);

            Assert.NotNull(result);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task UpdateUser_ValidUser_ReturnsUpdatedUser()
        {
            var user = new User
            {
                FirstName = "OriginalFirstName",
                LastName = "OriginalLastName",
                Email = "test@gmail.com",
                Password = "password"
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var userToUpdate = new User
            {
                Userid = user.Userid, // Dynamic userid from already added user
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated@gmail.com",
                Password = "newpassword"
            };

            var result = await _userRepository.UpdateUser(userToUpdate);

            Assert.NotNull(result);
            Assert.Equal(userToUpdate.FirstName, result.FirstName);
            Assert.Equal(userToUpdate.LastName, result.LastName);
            Assert.Equal(userToUpdate.Email, result.Email);
            Assert.Equal(userToUpdate.Password, result.Password);
        }
    }
}