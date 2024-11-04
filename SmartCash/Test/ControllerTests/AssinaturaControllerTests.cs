using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartCash.Controllers;
using SmartCash.Models;
using SmartCash.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class AssinaturaControllerTests
{
    private readonly Mock<AssinaturaRepository> _assinaturaRepositoryMock;
    private readonly AssinaturaController _controller;

    public AssinaturaControllerTests()
    {
        _assinaturaRepositoryMock = new Mock<AssinaturaRepository>();
        _controller = new AssinaturaController(_assinaturaRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAssinaturas_ReturnsOkResult_WithListOfAssinaturas()
    {
        var assinaturas = new List<Assinatura>
        {
            new Assinatura { IdAssinatura = 1},
            new Assinatura { IdAssinatura = 2}
        };

        _assinaturaRepositoryMock.Setup(repo => repo.GetAssinaturas()).ReturnsAsync(assinaturas);

        var result = await _controller.GetAssinaturas();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Assinatura>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetAssinatura_ReturnsNotFound_WhenAssinaturaDoesNotExist()
    {
        int assinaturaId = 1;
        _assinaturaRepositoryMock.Setup(repo => repo.GetAssinatura(assinaturaId)).ReturnsAsync((Assinatura)null);

        var result = await _controller.GetAssinatura(assinaturaId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddAssinatura_ReturnsCreatedAtAction_WithNewAssinatura()
    {
        var newAssinatura = new Assinatura {};
        _assinaturaRepositoryMock.Setup(repo => repo.AddAssinatura(newAssinatura)).ReturnsAsync(newAssinatura);

        var result = await _controller.AddAssinatura(newAssinatura);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Assinatura>(createdAtActionResult.Value);
        Assert.Equal(newAssinatura.IdAssinatura, returnValue.IdAssinatura);
    }

    [Fact]
    public async Task UpdateAssinatura_ReturnsNotFound_WhenAssinaturaDoesNotExist()
    {
        int assinaturaId = 1;
        var assinaturaToUpdate = new Assinatura { IdAssinatura = assinaturaId };
        _assinaturaRepositoryMock.Setup(repo => repo.GetAssinatura(assinaturaId)).ReturnsAsync((Assinatura)null);

        var result = await _controller.UpdateAssinatura(assinaturaId, assinaturaToUpdate);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteAssinatura_ReturnsOkResult_WhenSuccessfullyDeleted()
    {
        int assinaturaId = 1;
        var assinaturaToDelete = new Assinatura { IdAssinatura = assinaturaId };
        _assinaturaRepositoryMock.Setup(repo => repo.GetAssinatura(assinaturaId)).ReturnsAsync(assinaturaToDelete);
        _assinaturaRepositoryMock.Setup(repo => repo.DeleteAssinatura(assinaturaId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteAssinatura(assinaturaId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Assinatura com id {assinaturaId} deletada", okResult.Value);
    }

    [Fact]
    public async Task DeleteAssinatura_ReturnsNotFound_WhenAssinaturaDoesNotExist()
    {
        int assinaturaId = 1;
        _assinaturaRepositoryMock.Setup(repo => repo.GetAssinatura(assinaturaId)).ReturnsAsync((Assinatura)null);

        var result = await _controller.DeleteAssinatura(assinaturaId);

        Assert.IsType<NotFoundResult>(result);
    }
}
