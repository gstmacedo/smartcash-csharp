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

public class UsuarioEmpresaControllerTests
{
    private readonly Mock<UsuarioEmpresaRepository> _usuarioEmpresaRepositoryMock;
    private readonly UsuarioEmpresaController _controller;

    public UsuarioEmpresaControllerTests()
    {
        _usuarioEmpresaRepositoryMock = new Mock<UsuarioEmpresaRepository>();
        _controller = new UsuarioEmpresaController(_usuarioEmpresaRepositoryMock.Object);
    }

    [Fact]
    public async Task GetUsuarioEmpresas_ReturnsOkResult_WithListOfUsuarioEmpresas()
    {
        var usuarioEmpresas = new List<UsuarioEmpresa>
        {
            new UsuarioEmpresa { IdUsuarioEmpresa = 1},
            new UsuarioEmpresa { IdUsuarioEmpresa = 2}
        };

        _usuarioEmpresaRepositoryMock.Setup(repo => repo.GetUsuarioEmpresas()).ReturnsAsync(usuarioEmpresas);

        var result = await _controller.GetUsuarioEmpresas();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<UsuarioEmpresa>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetUsuarioEmpresa_ReturnsNotFound_WhenUsuarioEmpresaDoesNotExist()
    {
        int usuarioEmpresaId = 1;
        _usuarioEmpresaRepositoryMock.Setup(repo => repo.GetUsuarioEmpresa(usuarioEmpresaId)).ReturnsAsync((UsuarioEmpresa)null);

        var result = await _controller.GetUsuarioEmpresa(usuarioEmpresaId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddUsuarioEmpresa_ReturnsCreatedAtAction_WithNewUsuarioEmpresa()
    {
        var newUsuarioEmpresa = new UsuarioEmpresa {};
        _usuarioEmpresaRepositoryMock.Setup(repo => repo.AddUsuarioEmpresa(newUsuarioEmpresa)).ReturnsAsync(newUsuarioEmpresa);

        var result = await _controller.AddUsuarioEmpresa(newUsuarioEmpresa);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<UsuarioEmpresa>(createdAtActionResult.Value);
        Assert.Equal(newUsuarioEmpresa.IdUsuarioEmpresa, returnValue.IdUsuarioEmpresa);
    }

    [Fact]
    public async Task UpdateUsuarioEmpresa_ReturnsNotFound_WhenUsuarioEmpresaDoesNotExist()
    {
        int usuarioEmpresaId = 1;
        var usuarioEmpresaToUpdate = new UsuarioEmpresa { IdUsuarioEmpresa = usuarioEmpresaId };
        _usuarioEmpresaRepositoryMock.Setup(repo => repo.GetUsuarioEmpresa(usuarioEmpresaId)).ReturnsAsync((UsuarioEmpresa)null);

        var result = await _controller.UpdateUsuarioEmpresa(usuarioEmpresaId, usuarioEmpresaToUpdate);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteUsuarioEmpresa_ReturnsOkResult_WhenSuccessfullyDeleted()
    {
        int usuarioEmpresaId = 1;
        var usuarioEmpresaToDelete = new UsuarioEmpresa { IdUsuarioEmpresa = usuarioEmpresaId };
        _usuarioEmpresaRepositoryMock.Setup(repo => repo.GetUsuarioEmpresa(usuarioEmpresaId)).ReturnsAsync(usuarioEmpresaToDelete);
        _usuarioEmpresaRepositoryMock.Setup(repo => repo.DeleteUsuarioEmpresa(usuarioEmpresaId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteUsuarioEmpresa(usuarioEmpresaId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Usuário-empresa com id {usuarioEmpresaId} deletado", okResult.Value);
    }

    [Fact]
    public async Task DeleteUsuarioEmpresa_ReturnsNotFound_WhenUsuarioEmpresaDoesNotExist()
    {
        int usuarioEmpresaId = 1;
        _usuarioEmpresaRepositoryMock.Setup(repo => repo.GetUsuarioEmpresa(usuarioEmpresaId)).ReturnsAsync((UsuarioEmpresa)null);

        var result = await _controller.DeleteUsuarioEmpresa(usuarioEmpresaId);

        Assert.IsType<NotFoundResult>(result);
    }
}
