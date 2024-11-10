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
    public class GameServiceTests
    {
        private Mock<IRepository<Game>> _mockGameRepository;
        private GameService _gameService;

        [TestInitialize]
        public void Setup()
        {
            // Inicialização dos mocks e do serviço
            _mockGameRepository = new Mock<IRepository<Game>>();
            _gameService = new GameService(_mockGameRepository.Object);
        }

        #region RegisterGame Tests
        [TestMethod]
        public async Task RegisterGame_ShouldReturnTrue_WhenGameIsRegistered()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "FIFA 2024" };
            _mockGameRepository.Setup(r => r.Insert(It.IsAny<Game>())).ReturnsAsync(true);

            // Act
            var result = await _gameService.RegisterGame(game);

            // Assert
            Assert.IsTrue(result);
            _mockGameRepository.Verify(r => r.Insert(It.IsAny<Game>()), Times.Once);
        }

        [TestMethod]
        public async Task RegisterGame_ShouldReturnFalse_WhenGameRegistrationFails()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "FIFA 2024" };
            _mockGameRepository.Setup(r => r.Insert(It.IsAny<Game>())).ReturnsAsync(false);

            // Act
            var result = await _gameService.RegisterGame(game);

            // Assert
            Assert.IsFalse(result);
            _mockGameRepository.Verify(r => r.Insert(It.IsAny<Game>()), Times.Once);
        }
        #endregion

        #region DeleteGame Tests
        [TestMethod]
        public async Task DeleteGame_ShouldReturnTrue_WhenGameIsDeleted()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "GTA 5" };
            _mockGameRepository.Setup(r => r.Delete(game.Id)).ReturnsAsync(true);

            // Act
            var result = await _gameService.DeleteGame(game);

            // Assert
            Assert.IsTrue(result);
            _mockGameRepository.Verify(r => r.Delete(game.Id), Times.Once);
        }

        [TestMethod]
        public async Task DeleteGame_ShouldReturnFalse_WhenGameDeletionFails()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "GTA 5" };
            _mockGameRepository.Setup(r => r.Delete(game.Id)).ReturnsAsync(false);

            // Act
            var result = await _gameService.DeleteGame(game);

            // Assert
            Assert.IsFalse(result);
            _mockGameRepository.Verify(r => r.Delete(game.Id), Times.Once);
        }
        #endregion

        #region getAllGame Tests
        [TestMethod]
        public async Task getAllGame_ShouldReturnListOfGames_WhenGamesExist()
        {
            // Arrange
            var games = new List<Game>
            {
                new Game { Id = 1, Name = "FIFA 2024" },
                new Game { Id = 2, Name = "Call of Duty" }
            };
            _mockGameRepository.Setup(r => r.GetAll()).ReturnsAsync(games);

            // Act
            var result = await _gameService.getAllGame();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            _mockGameRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public async Task getAllGame_ShouldReturnEmptyList_WhenNoGamesExist()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<Game>());

            // Act
            var result = await _gameService.getAllGame();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            _mockGameRepository.Verify(r => r.GetAll(), Times.Once);
        }
        #endregion

        #region getGameById Tests
        [TestMethod]
        public async Task getGameById_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "FIFA 2024" };
            _mockGameRepository.Setup(r => r.Get(1)).ReturnsAsync(game);

            // Act
            var result = await _gameService.getGameById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("FIFA 2024", result.Name);
            _mockGameRepository.Verify(r => r.Get(1), Times.Once);
        }

        [TestMethod]
        public async Task getGameById_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.Get(999)).ReturnsAsync((Game)null);

            // Act
            var result = await _gameService.getGameById(999);

            // Assert
            Assert.IsNull(result);
            _mockGameRepository.Verify(r => r.Get(999), Times.Once);
        }
        #endregion

        #region getGameByName Tests
        [TestMethod]
        public async Task getGameByName_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "FIFA 2024" };
            _mockGameRepository.Setup(r => r.Get("FIFA 2024")).ReturnsAsync(game);

            // Act
            var result = await _gameService.getGameByName("FIFA 2024");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("FIFA 2024", result.Name);
            _mockGameRepository.Verify(r => r.Get("FIFA 2024"), Times.Once);
        }

        [TestMethod]
        public async Task getGameByName_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.Get("NonExistent Game")).ReturnsAsync((Game)null);

            // Act
            var result = await _gameService.getGameByName("NonExistent Game");

            // Assert
            Assert.IsNull(result);
            _mockGameRepository.Verify(r => r.Get("NonExistent Game"), Times.Once);
        }
        #endregion

        #region UpdateGame Tests
        [TestMethod]
        public async Task UpdateGame_ShouldReturnTrue_WhenGameIsUpdatedSuccessfully()
        {
            // Arrange
            var existingGame = new Game { Id = 1, Name = "FIFA 2024" };
            var updatedGame = new Game { Id = 1, Name = "FIFA 2025" };

            // Simula que o jogo já existe no repositório
            _mockGameRepository.Setup(r => r.Get(1)).ReturnsAsync(existingGame);
            _mockGameRepository.Setup(r => r.Update(updatedGame)).ReturnsAsync(true);

            // Act
            var result = await _gameService.UpdateGame(updatedGame);

            // Assert
            Assert.IsTrue(result);
            _mockGameRepository.Verify(r => r.Update(updatedGame), Times.Once);
        }

        [TestMethod]
        public async Task UpdateGame_ShouldReturnFalse_WhenGameDoesNotExist()
        {
            // Arrange
            var updatedGame = new Game { Id = 999, Name = "NonExistent Game" };

            // Simula que o jogo não existe no repositório
            _mockGameRepository.Setup(r => r.Get(999)).ReturnsAsync((Game)null);

            // Act
            var result = await _gameService.UpdateGame(updatedGame);

            // Assert
            Assert.IsFalse(result);
            _mockGameRepository.Verify(r => r.Update(It.IsAny<Game>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateGame_ShouldReturnFalse_WhenGameUpdateFails()
        {
            // Arrange
            var existingGame = new Game { Id = 1, Name = "FIFA 2024" };
            var updatedGame = new Game { Id = 1, Name = "FIFA 2025" };

            // Simula que o jogo já existe no repositório, mas a atualização falha
            _mockGameRepository.Setup(r => r.Get(1)).ReturnsAsync(existingGame);
            _mockGameRepository.Setup(r => r.Update(updatedGame)).ReturnsAsync(false);

            // Act
            var result = await _gameService.UpdateGame(updatedGame);

            // Assert
            Assert.IsFalse(result);
            _mockGameRepository.Verify(r => r.Update(updatedGame), Times.Once);
        }
        #endregion

        #region InitialData Tests
        [TestMethod]
        public async Task InitialData_ShouldInsertGamesSuccessfully()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.Get(It.IsAny<string>())).ReturnsAsync((Game)null);
            _mockGameRepository.Setup(r => r.Insert(It.IsAny<Game>())).ReturnsAsync(true);

            // Act
            var result = await _gameService.InitialData();

            // Assert
            Assert.IsTrue(result);
            _mockGameRepository.Verify(r => r.Insert(It.IsAny<Game>()), Times.Exactly(5));
        }
        #endregion
    }
}
