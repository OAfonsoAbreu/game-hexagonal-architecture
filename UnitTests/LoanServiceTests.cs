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
    public class LoanServiceTests
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IRepository<Game>> _mockGameRepository;
        private Mock<ILoanRepository> _mockLoanRepository;
        private LoanService _loanService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockGameRepository = new Mock<IRepository<Game>>();
            _mockLoanRepository = new Mock<ILoanRepository>();
            _loanService = new LoanService(_mockUserRepository.Object, _mockGameRepository.Object, _mockLoanRepository.Object);
        }

        #region RegisterLoan Tests
        [TestMethod]
        public async Task RegisterLoan_ShouldReturnTrue_WhenLoanIsRegistered()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "John" }, Game = new Game { Name = "FIFA 2024" } };
            _mockUserRepository.Setup(r => r.Get("John")).ReturnsAsync(new User { Name = "John" });
            _mockGameRepository.Setup(r => r.Get("FIFA 2024")).ReturnsAsync(new Game { Name = "FIFA 2024" });
            _mockLoanRepository.Setup(r => r.InsertOrUpdate(It.IsAny<Loan>())).ReturnsAsync(true);

            // Act
            var result = await _loanService.RegisterLoan(loan);

            // Assert
            Assert.IsTrue(result);
            _mockLoanRepository.Verify(r => r.InsertOrUpdate(It.IsAny<Loan>()), Times.Once);
        }

        [TestMethod]
        public async Task RegisterLoan_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "NonExistentUser" }, Game = new Game { Name = "FIFA 2024" } };
            _mockUserRepository.Setup(r => r.Get("NonExistentUser")).ReturnsAsync((User)null);

            // Act
            var result = await _loanService.RegisterLoan(loan);

            // Assert
            Assert.IsFalse(result);
            _mockLoanRepository.Verify(r => r.InsertOrUpdate(It.IsAny<Loan>()), Times.Never);
        }

        [TestMethod]
        public async Task RegisterLoan_ShouldReturnFalse_WhenGameNotFound()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "John" }, Game = new Game { Name = "NonExistentGame" } };
            _mockUserRepository.Setup(r => r.Get("John")).ReturnsAsync(new User { Name = "John" });
            _mockGameRepository.Setup(r => r.Get("NonExistentGame")).ReturnsAsync((Game)null);

            // Act
            var result = await _loanService.RegisterLoan(loan);

            // Assert
            Assert.IsFalse(result);
            _mockLoanRepository.Verify(r => r.InsertOrUpdate(It.IsAny<Loan>()), Times.Never);
        }
        #endregion

        #region UpdateLoan Tests
        [TestMethod]
        public async Task UpdateLoan_ShouldReturnTrue_WhenLoanIsUpdated()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "John" }, Game = new Game { Name = "FIFA 2024" } };
            _mockUserRepository.Setup(r => r.Get("John")).ReturnsAsync(new User { Name = "John" });
            _mockGameRepository.Setup(r => r.Get("FIFA 2024")).ReturnsAsync(new Game { Name = "FIFA 2024" });
            _mockLoanRepository.Setup(r => r.InsertOrUpdate(It.IsAny<Loan>())).ReturnsAsync(true);

            // Act
            var result = await _loanService.UpdateLoan(loan);

            // Assert
            Assert.IsTrue(result);
            _mockLoanRepository.Verify(r => r.InsertOrUpdate(It.IsAny<Loan>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateLoan_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "NonExistentUser" }, Game = new Game { Name = "FIFA 2024" } };
            _mockUserRepository.Setup(r => r.Get("NonExistentUser")).ReturnsAsync((User)null);

            // Act
            var result = await _loanService.UpdateLoan(loan);

            // Assert
            Assert.IsFalse(result);
            _mockLoanRepository.Verify(r => r.InsertOrUpdate(It.IsAny<Loan>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdateLoan_ShouldReturnFalse_WhenGameNotFound()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "John" }, Game = new Game { Name = "NonExistentGame" } };
            _mockUserRepository.Setup(r => r.Get("John")).ReturnsAsync(new User { Name = "John" });
            _mockGameRepository.Setup(r => r.Get("NonExistentGame")).ReturnsAsync((Game)null);

            // Act
            var result = await _loanService.UpdateLoan(loan);

            // Assert
            Assert.IsFalse(result);
            _mockLoanRepository.Verify(r => r.InsertOrUpdate(It.IsAny<Loan>()), Times.Never);
        }
        #endregion

        #region DeleteLoan Tests
        [TestMethod]
        public async Task DeleteLoan_ShouldReturnTrue_WhenLoanExistsAndIsDeleted()
        {
            // Arrange
            var loan = new Loan { Id = 1 };
            _mockLoanRepository.Setup(r => r.Get(loan.Id)).ReturnsAsync(loan);
            _mockLoanRepository.Setup(r => r.Delete(loan.Id)).ReturnsAsync(true);

            // Act
            var result = await _loanService.DeleteLoan(loan);

            // Assert
            Assert.IsTrue(result);
            _mockLoanRepository.Verify(r => r.Delete(loan.Id), Times.Once);
        }

        [TestMethod]
        public async Task DeleteLoan_ShouldReturnFalse_WhenLoanDoesNotExist()
        {
            // Arrange
            var loan = new Loan { Id = 1 };
            _mockLoanRepository.Setup(r => r.Get(loan.Id)).ReturnsAsync((Loan)null);

            // Act
            var result = await _loanService.DeleteLoan(loan);

            // Assert
            Assert.IsFalse(result);
            _mockLoanRepository.Verify(r => r.Delete(loan.Id), Times.Never);
        }
        #endregion

        #region getLoanById Tests
        [TestMethod]
        public async Task getLoanById_ShouldReturnLoan_WhenLoanExists()
        {
            // Arrange
            var loan = new Loan { Id = 1 };
            _mockLoanRepository.Setup(r => r.Get(1)).ReturnsAsync(loan);

            // Act
            var result = await _loanService.getLoanById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            _mockLoanRepository.Verify(r => r.Get(1), Times.Once);
        }

        [TestMethod]
        public async Task getLoanById_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            // Arrange
            _mockLoanRepository.Setup(r => r.Get(999)).ReturnsAsync((Loan)null);

            // Act
            var result = await _loanService.getLoanById(999);

            // Assert
            Assert.IsNull(result);
            _mockLoanRepository.Verify(r => r.Get(999), Times.Once);
        }
        #endregion

        #region getLoanByName Tests
        [TestMethod]
        public async Task getLoanByName_ShouldReturnLoan_WhenLoanExists()
        {
            // Arrange
            var loan = new Loan { Id = 1, User = new User { Name = "John" }, Game = new Game { Name = "FIFA 2024" } };
            _mockLoanRepository.Setup(r => r.Get("John", "FIFA 2024")).ReturnsAsync(loan);

            // Act
            var result = await _loanService.getLoanByName("John", "FIFA 2024");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.User.Name);
            Assert.AreEqual("FIFA 2024", result.Game.Name);
            _mockLoanRepository.Verify(r => r.Get("John", "FIFA 2024"), Times.Once);
        }

        [TestMethod]
        public async Task getLoanByName_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            // Arrange
            _mockLoanRepository.Setup(r => r.Get("NonExistentUser", "NonExistentGame")).ReturnsAsync((Loan)null);

            // Act
            var result = await _loanService.getLoanByName("NonExistentUser", "NonExistentGame");

            // Assert
            Assert.IsNull(result);
            _mockLoanRepository.Verify(r => r.Get("NonExistentUser", "NonExistentGame"), Times.Once);
        }
        #endregion

        #region getAllLoans Tests
        [TestMethod]
        public async Task getAllLoans_ShouldReturnListOfLoans_WhenLoansExist()
        {
            // Arrange
            var loans = new List<Loan>
            {
                new Loan { Id = 1, User = new User { Name = "John" }, Game = new Game { Name = "FIFA 2024" } },
                new Loan { Id = 2, User = new User { Name = "Alice" }, Game = new Game { Name = "GTA 5" } }
            };
            _mockLoanRepository.Setup(r => r.GetAll()).ReturnsAsync(loans);

            // Act
            var result = await _loanService.getAllLoans();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            _mockLoanRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public async Task getAllLoans_ShouldReturnEmptyList_WhenNoLoansExist()
        {
            // Arrange
            _mockLoanRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<Loan>());

            // Act
            var result = await _loanService.getAllLoans();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            _mockLoanRepository.Verify(r => r.GetAll(), Times.Once);
        }
        #endregion
    }
}
