using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.ContentModel;
using SmartCash.Controllers;
using SmartCash.Models;
using SmartCash.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class FluxoCaixaControllerTests
{
    private readonly Mock<FluxoCaixaRepository> _fluxoCaixaRepositoryMock;
    private readonly FluxoCaixaController _controller;

    public FluxoCaixaControllerTests()
    {
        _fluxoCaixaRepositoryMock = new Mock<FluxoCaixaRepository>();
        _controller = new FluxoCaixaController(_fluxoCaixaRepositoryMock.Object);
    }

    [Fact]
    public async Task GetFluxoCaixas_ReturnsOkResult_WithListOfFluxoCaixa()
    {
        var fluxoCaixas = new List<FluxoCaixa>
        {
            new FluxoCaixa { IdFluxo = 1 },
            new FluxoCaixa { IdFluxo = 2 }
        };

        _fluxoCaixaRepositoryMock.Setup(repo => repo.GetFluxoCaixas()).ReturnsAsync(fluxoCaixas);

        var result = await _controller.GetFluxoCaixas();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<FluxoCaixa>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetFluxoCaixa_ReturnsNotFound_WhenFluxoCaixaDoesNotExist()
    {
        int fluxoCaixaId = 1;
        _fluxoCaixaRepositoryMock.Setup(repo => repo.GetFluxoCaixa(fluxoCaixaId)).ReturnsAsync((FluxoCaixa)null);

        var result = await _controller.GetFluxoCaixa(fluxoCaixaId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddFluxoCaixa_ReturnsCreatedAtAction_WithNewFluxoCaixa()
    {
        var newFluxoCaixa = new FluxoCaixa {};
        _fluxoCaixaRepositoryMock.Setup(repo => repo.AddFluxoCaixa(newFluxoCaixa)).ReturnsAsync(newFluxoCaixa);

        var result = await _controller.AddFluxoCaixa(newFluxoCaixa);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<FluxoCaixa>(createdAtActionResult.Value);
        Assert.Equal(newFluxoCaixa.IdFluxo, returnValue.IdFluxo);
    }

    [Fact]
    public async Task UpdateFluxoCaixa_ReturnsNotFound_WhenFluxoCaixaDoesNotExist()
    {
        int fluxoCaixaId = 1;
        var fluxoCaixaToUpdate = new FluxoCaixa { IdFluxo = fluxoCaixaId };
        _fluxoCaixaRepositoryMock.Setup(repo => repo.GetFluxoCaixa(fluxoCaixaId)).ReturnsAsync((FluxoCaixa)null);

        var result = await _controller.UpdateFluxoCaixa(fluxoCaixaId, fluxoCaixaToUpdate);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteFluxoCaixa_ReturnsOkResult_WhenSuccessfullyDeleted()
    {
        int fluxoCaixaId = 1;
        var fluxoCaixaToDelete = new FluxoCaixa { IdFluxo = fluxoCaixaId };
        _fluxoCaixaRepositoryMock.Setup(repo => repo.GetFluxoCaixa(fluxoCaixaId)).ReturnsAsync(fluxoCaixaToDelete);
        _fluxoCaixaRepositoryMock.Setup(repo => repo.DeleteFluxoCaixa(fluxoCaixaId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteFluxoCaixa(fluxoCaixaId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Fluxo de caixa com id {fluxoCaixaId} deletado", okResult.Value);
    }

    [Fact]
    public async Task DeleteFluxoCaixa_ReturnsNotFound_WhenFluxoCaixaDoesNotExist()
    {
        int fluxoCaixaId = 1;
        _fluxoCaixaRepositoryMock.Setup(repo => repo.GetFluxoCaixa(fluxoCaixaId)).ReturnsAsync((FluxoCaixa)null);

        var result = await _controller.DeleteFluxoCaixa(fluxoCaixaId);

        Assert.IsType<NotFoundResult>(result);
    }
}
