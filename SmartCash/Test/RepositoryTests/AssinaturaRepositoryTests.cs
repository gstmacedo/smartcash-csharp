using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartCash.Data;
using SmartCash.Models;
using SmartCash.Repository;
using System.Linq;

public class AssinaturaRepositoryTests
{
    private readonly AssinaturaRepository _repository;
    private readonly Mock<DbSet<Assinatura>> _mockSet;
    private readonly Mock<dbContext> _mockContext;

    public AssinaturaRepositoryTests()
    {
        _mockSet = new Mock<DbSet<Assinatura>>();
        _mockContext = new Mock<dbContext>();
        _mockContext.Setup(m => m.Assinaturas).Returns(_mockSet.Object);
        _repository = new AssinaturaRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddAssinatura_AddsAssinatura()
    {
        var assinatura = new Assinatura { IdAssinatura = 1, Tipo = "Test", Valor = 100 };

        await _repository.AddAssinatura(assinatura);

        _mockSet.Verify(m => m.AddAsync(assinatura, default), Times.Once());
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task GetAssinatura_ReturnsAssinatura()
    {
        var assinatura = new Assinatura { IdAssinatura = 1, Tipo = "Test", Valor = 100 };
        var data = new List<Assinatura> { assinatura }.AsQueryable();

        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var result = await _repository.GetAssinatura(1);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Tipo);
    }

    [Fact]
    public async Task UpdateAssinatura_UpdatesAssinatura()
    {
        var assinatura = new Assinatura { IdAssinatura = 1, Tipo = "Test", Valor = 100 };
        _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(assinatura);

        var updatedAssinatura = new Assinatura { IdAssinatura = 1, Tipo = "Updated", Valor = 200 };

        var result = await _repository.UpdateAssinatura(updatedAssinatura);

        Assert.NotNull(result);
        Assert.Equal("Updated", result.Tipo);
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task DeleteAssinatura_RemovesAssinatura()
    {
        var assinatura = new Assinatura { IdAssinatura = 1, Tipo = "Test", Valor = 100 };
        _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(assinatura);

        await _repository.DeleteAssinatura(1);

        _mockSet.Verify(m => m.Remove(assinatura), Times.Once());
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task GetAssinaturas_ReturnsAllAssinaturas()
    {
        var assinatura1 = new Assinatura { IdAssinatura = 1, Tipo = "Test1", Valor = 100 };
        var assinatura2 = new Assinatura { IdAssinatura = 2, Tipo = "Test2", Valor = 200 };
        var data = new List<Assinatura> { assinatura1, assinatura2 }.AsQueryable();

        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<Assinatura>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var result = await _repository.GetAssinaturas();

        Assert.Equal(2, result.Count());
    }
}
