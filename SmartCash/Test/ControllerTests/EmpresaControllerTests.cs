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

public class EmpresaControllerTests
{
    private readonly Mock<EmpresaRepository> _empresaRepositoryMock;
    private readonly EmpresaController _controller;

    public EmpresaControllerTests()
    {
        _empresaRepositoryMock = new Mock<EmpresaRepository>();
        _controller = new EmpresaController(_empresaRepositoryMock.Object);
    }

    [Fact]
    public async Task GetEmpresas_ReturnsOkResult_WithListOfEmpresas()
    {
        var empresas = new List<Empresa>
        {
            new Empresa { IdEmpresa = 1 },
            new Empresa { IdEmpresa = 2,}
        };

        _empresaRepositoryMock.Setup(repo => repo.GetEmpresas()).ReturnsAsync(empresas);

        var result = await _controller.GetEmpresas();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Empresa>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
    {
        int empresaId = 1;
        _empresaRepositoryMock.Setup(repo => repo.GetEmpresa(empresaId)).ReturnsAsync((Empresa)null);

        var result = await _controller.GetEmpresa(empresaId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddEmpresa_ReturnsCreatedAtAction_WithNewEmpresa()
    {
        var newEmpresa = new Empresa {};
        _empresaRepositoryMock.Setup(repo => repo.AddEmpresa(newEmpresa)).ReturnsAsync(newEmpresa);

        var result = await _controller.AddEmpresa(newEmpresa);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Empresa>(createdAtActionResult.Value);
        Assert.Equal(newEmpresa.IdEmpresa, returnValue.IdEmpresa);
    }

    [Fact]
    public async Task UpdateEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
    {
        int empresaId = 1;
        var empresaToUpdate = new Empresa { IdEmpresa = empresaId };
        _empresaRepositoryMock.Setup(repo => repo.GetEmpresa(empresaId)).ReturnsAsync((Empresa)null);

        var result = await _controller.UpdateEmpresa(empresaId, empresaToUpdate);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteEmpresa_ReturnsOkResult_WhenSuccessfullyDeleted()
    {
        int empresaId = 1;
        var empresaToDelete = new Empresa { IdEmpresa = empresaId };
        _empresaRepositoryMock.Setup(repo => repo.GetEmpresa(empresaId)).ReturnsAsync(empresaToDelete);
        _empresaRepositoryMock.Setup(repo => repo.DeleteEmpresa(empresaId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteEmpresa(empresaId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Empresa com id {empresaId} deletada", okResult.Value);
    }

    [Fact]
    public async Task DeleteEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
    {
        int empresaId = 1;
        _empresaRepositoryMock.Setup(repo => repo.GetEmpresa(empresaId)).ReturnsAsync((Empresa)null);

        var result = await _controller.DeleteEmpresa(empresaId);

        Assert.IsType<NotFoundResult>(result);
    }
}
