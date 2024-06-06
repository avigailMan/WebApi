using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UserRepositoryUnit
    {
        [Fact]
        // login happy
        public async Task GetUser_ValidCredentials_ReturnUser()
        {
            var user = new User { Email = "test@example.com", Password = "password" };

            var mockContex = new Mock<ProductDbContext>();
            var users = new List<User>() { user };
            mockContex.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContex.Object);

            var result = await userRepository.Login(user.Email, user.Password);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task AddUser_ValidUser_ReturnsAddedUser()
        {
            // Arrange
            var user = new User
            {
                Userid = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "test@gmail.com",
                Password = "password"
            };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<ProductDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.AddUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Password, result.Password);

            mockSet.Verify(m => m.AddAsync(user, default), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ValidUser_ReturnsUpdatedUser()
        {
            // Arrange
            var user = new User
            {
                Userid = 1,
                FirstName = "OriginalFirstName",
                LastName = "OriginalLastName",
                Email = "original@example.com",
                Password = "originalpassword"
            };

            var userToUpdate = new User
            {
                Userid = 1,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated@example.com",
                Password = "updatedpassword"
            };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<ProductDbContext>();

            mockSet.Setup(m => m.FindAsync(userToUpdate.Userid)).ReturnsAsync(user);
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.UpdateUser(userToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userToUpdate.FirstName, result.FirstName);
            Assert.Equal(userToUpdate.LastName, result.LastName);
            Assert.Equal(userToUpdate.Email, result.Email);
            Assert.Equal(userToUpdate.Password, result.Password);

            mockSet.Verify(m => m.Update(user), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}