using Application;
using Application.Services;
using Domain.Adapters;
using Domain.Entities;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace UnitTests
{
    [TestClass]
    public class AuthServiceTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _authService = new AuthService(_mockAccountRepository.Object);
        }

        // Teste para o m�todo GenerateToken
        [TestMethod]
        public void GenerateToken_ShouldReturnValidJwtToken()
        {
            // Arrange
            var account = new Account
            {
                Username = "user1",
                Role = "Admin"
            };

            // Mock do Settings.Secret (substituindo o valor real da chave secreta)
            const string secret = "fedaf7d8863b48e197b9287d492b708e";
            Settings.Secret = secret;

            // Act
            var token = _authService.GenerateToken(account);

            // Assert
            Assert.IsNotNull(token);

            // Validando o formato do token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            Assert.IsNotNull(parsedToken);
            Assert.AreEqual(account.Username, parsedToken?.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);
            Assert.AreEqual(account.Role, parsedToken?.Claims.FirstOrDefault(c => c.Type == "role")?.Value);

        }

        // Teste para o m�todo GetAccount com credenciais v�lidas
        [TestMethod]
        public async Task GetAccount_ShouldReturnAccount_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "user1";
            var password = "password123";

            var expectedAccount = new Account
            {
                Id = 1,
                Username = username,
                Password = password, // A senha tamb�m � passada aqui, mas n�o ser� usada para o token
                Role = "Admin"
            };

            // Mock do reposit�rio para retornar a conta quando as credenciais forem v�lidas
            _mockAccountRepository.Setup(repo => repo.Get(username, password))
                                  .ReturnsAsync(expectedAccount);

            // Act
            var result = await _authService.GetAccount(username, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAccount.Username, result.Username);
            Assert.AreEqual(expectedAccount.Role, result.Role);
            Assert.AreEqual(expectedAccount.Password, result.Password); // Verifique se a senha tamb�m corresponde
        }

        // Teste para o m�todo GetAccount com credenciais inv�lidas
        [TestMethod]
        public async Task GetAccount_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var username = "user1";
            var password = "wrongpassword";

            // Mock do reposit�rio para retornar null quando as credenciais forem inv�lidas
            _mockAccountRepository.Setup(repo => repo.Get(username, password))
                                  .ReturnsAsync((Account)null);

            // Act
            var result = await _authService.GetAccount(username, password);

            // Assert
            Assert.IsNull(result);
        }
    }
}