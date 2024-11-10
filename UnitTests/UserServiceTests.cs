using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Domain.Adapters;

namespace UnitTests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        #region RegisterUser Tests
        [TestMethod]
        public async Task RegisterUser_ShouldReturnTrue_WhenUserIsRegistered()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _mockUserRepository.Setup(r => r.Insert(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _userService.RegisterUser(user);

            // Assert
            Assert.IsTrue(result);
            _mockUserRepository.Verify(r => r.Insert(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task RegisterUser_ShouldReturnFalse_WhenUserRegistrationFails()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _mockUserRepository.Setup(r => r.Insert(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await _userService.RegisterUser(user);

            // Assert
            Assert.IsFalse(result);
            _mockUserRepository.Verify(r => r.Insert(It.IsAny<User>()), Times.Once);
        }
        #endregion

        #region UpdateUser Tests
        [TestMethod]
        public async Task UpdateUser_ShouldReturnTrue_WhenUserIsUpdated()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateUser(user);

            // Assert
            Assert.IsTrue(result);
            _mockUserRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldReturnFalse_WhenUserUpdateFails()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await _userService.UpdateUser(user);

            // Assert
            Assert.IsFalse(result);
            _mockUserRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }
        #endregion

        #region DeleteAUser Tests
        [TestMethod]
        public async Task DeleteAUser_ShouldReturnTrue_WhenUserIsDeleted()
        {
            // Arrange
            var user = new User { Id = 1 };
            _mockUserRepository.Setup(r => r.Delete(user.Id)).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteAUser(user);

            // Assert
            Assert.IsTrue(result);
            _mockUserRepository.Verify(r => r.Delete(user.Id), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAUser_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new User { Id = 1 };
            _mockUserRepository.Setup(r => r.Delete(user.Id)).ReturnsAsync(false);

            // Act
            var result = await _userService.DeleteAUser(user);

            // Assert
            Assert.IsFalse(result);
            _mockUserRepository.Verify(r => r.Delete(user.Id), Times.Once);
        }
        #endregion

        #region getUserById Tests
        [TestMethod]
        public async Task getUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _mockUserRepository.Setup(r => r.Get(1)).ReturnsAsync(user);

            // Act
            var result = await _userService.getUserById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("John", result.Name);
            _mockUserRepository.Verify(r => r.Get(1), Times.Once);
        }

        [TestMethod]
        public async Task getUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.Get(999)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.getUserById(999);

            // Assert
            Assert.IsNull(result);
            _mockUserRepository.Verify(r => r.Get(999), Times.Once);
        }
        #endregion

        #region getUserByName Tests
        [TestMethod]
        public async Task getUserByName_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _mockUserRepository.Setup(r => r.Get("John")).ReturnsAsync(user);

            // Act
            var result = await _userService.getUserByName("John");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("John", result.Name);
            _mockUserRepository.Verify(r => r.Get("John"), Times.Once);
        }

        [TestMethod]
        public async Task getUserByName_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.Get("NonExistentUser")).ReturnsAsync((User)null);

            // Act
            var result = await _userService.getUserByName("NonExistentUser");

            // Assert
            Assert.IsNull(result);
            _mockUserRepository.Verify(r => r.Get("NonExistentUser"), Times.Once);
        }
        #endregion

        #region getAllUser Tests
        [TestMethod]
        public async Task getAllUser_ShouldReturnListOfUsers_WhenUsersExist()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Alice" }
            };
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _userService.getAllUser();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            _mockUserRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public async Task getAllUser_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<User>());

            // Act
            var result = await _userService.getAllUser();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            _mockUserRepository.Verify(r => r.GetAll(), Times.Once);
        }
        #endregion

        #region InitialData Tests
        [TestMethod]
        public async Task InitialData_ShouldReturnTrue_WhenDataIsInserted()
        {
            // Arrange
            var mockUserRepository = new Mock<IRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            // Simulando sucesso na inserção de dados iniciais
            mockUserRepository.Setup(repo => repo.Insert(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await userService.InitialData();

            // Assert
            Assert.IsTrue(result);
        }
        #endregion
    }
}
