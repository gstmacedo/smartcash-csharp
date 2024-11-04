using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartCash.Controllers;
using SmartCash.Models;
using SmartCash.Services;
using System.Threading.Tasks;
using Xunit;

public class AuthControllerTests
{
    private readonly Mock<AuthService> _authServiceMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<AuthService>();
        _authController = new AuthController(_authServiceMock.Object);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        var loginRequest = new Login { Email = "usuario@example.com", Senha = "senha123" };
        var mockUsuario = new Usuario { Email = loginRequest.Email };
        _authServiceMock.Setup(service => service.Authenticate(loginRequest.Email, loginRequest.Senha))
                         .ReturnsAsync(mockUsuario);

        var result = await _authController.Login(loginRequest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Login bem-sucedido.", result.Value);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        var loginRequest = new Login { Email = "usuario@example.com", Senha = "senhaErrada" };
        _authServiceMock.Setup(service => service.Authenticate(loginRequest.Email, loginRequest.Senha))
                         .ReturnsAsync((Usuario)null);

        var result = await _authController.Login(loginRequest);

        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Credenciais inválidas.", unauthorizedResult.Value);
    }

    [Fact]
    public async Task Login_NullRequest_ReturnsBadRequest()
    {
        Login loginRequest = null;

        var result = await _authController.Login(loginRequest);

        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }
}
