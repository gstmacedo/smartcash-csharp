using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using SmartCash.Repository;
using Xunit;

public class UsuarioRepositoryTests
{
    private readonly UsuarioRepository _repository;
    private readonly Mock<DbSet<Usuario>> _mockSet;
    private readonly Mock<dbContext> _mockContext;

    public UsuarioRepositoryTests()
    {
        _mockSet = new Mock<DbSet<Usuario>>();
        _mockContext = new Mock<dbContext>();
        _mockContext.Setup(m => m.Usuarios).Returns(_mockSet.Object);
        _repository = new UsuarioRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddUsuario_AddsUsuario()
    {
        var usuario = new Usuario { IdUsuario = 1, Nome = "Test User", Documento = "123.456.789-00", Email = "testuser@example.com", SenhaHash = "hashedPassword" };

        await _repository.AddUsuario(usuario);

        _mockSet.Verify(m => m.AddAsync(usuario, default), Times.Once());
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task GetUsuario_ReturnsUsuario()
    {
        var usuario = new Usuario { IdUsuario = 1, Nome = "Test User", Documento = "123.456.789-00", Email = "testuser@example.com", SenhaHash = "hashedPassword" };
        var data = new List<Usuario> { usuario }.AsQueryable();

        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var result = await _repository.GetUsuario(1);

        Assert.NotNull(result);
        Assert.Equal("Test User", result.Nome);
    }

    [Fact]
    public async Task UpdateUsuario_UpdatesUsuario()
    {
        var usuario = new Usuario { IdUsuario = 1, Nome = "Test User", Documento = "123.456.789-00", Email = "testuser@example.com", SenhaHash = "hashedPassword" };
        _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(usuario);

        var updatedUsuario = new Usuario { IdUsuario = 1, Nome = "Updated User", Documento = "123.456.789-00", Email = "updateduser@example.com", SenhaHash = "hashedPassword" };

        var result = await _repository.UpdateUsuario(updatedUsuario);

        Assert.NotNull(result);
        Assert.Equal("Updated User", result.Nome);
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task DeleteUsuario_RemovesUsuario()
    {
        var usuario = new Usuario { IdUsuario = 1, Nome = "Test User", Documento = "123.456.789-00", Email = "testuser@example.com", SenhaHash = "hashedPassword" };
        _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(usuario);

        await _repository.DeleteUsuario(1);

        _mockSet.Verify(m => m.Remove(usuario), Times.Once());
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task GetUsuarios_ReturnsAllUsuarios()
    {
        var usuario1 = new Usuario { IdUsuario = 1, Nome = "Test User 1", Documento = "123.456.789-00", Email = "testuser1@example.com", SenhaHash = "hashedPassword" };
        var usuario2 = new Usuario { IdUsuario = 2, Nome = "Test User 2", Documento = "987.654.321-00", Email = "testuser2@example.com", SenhaHash = "hashedPassword" };
        var data = new List<Usuario> { usuario1, usuario2 }.AsQueryable();

        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var result = await _repository.GetUsuarios();

        Assert.Equal(2, result.Count());
    }
}
