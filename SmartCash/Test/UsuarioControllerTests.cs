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

public class UsuarioControllerTests
{
    private readonly Mock<UsuarioRepository> _usuarioRepositoryMock;
    private readonly UsuarioController _controller;

    public UsuarioControllerTests()
    {
        _usuarioRepositoryMock = new Mock<UsuarioRepository>();
        _controller = new UsuarioController(_usuarioRepositoryMock.Object);
    }

    [Fact]
    public async Task GetUsuarios_ReturnsOkResult_WithListOfUsuarios()
    {
        var usuarios = new List<Usuario>
        {
            new Usuario { IdUsuario = 1},
            new Usuario { IdUsuario = 2}
        };

        _usuarioRepositoryMock.Setup(repo => repo.GetUsuarios()).ReturnsAsync(usuarios);

        var result = await _controller.GetUsuarios();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Usuario>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
    {
        int usuarioId = 1;
        _usuarioRepositoryMock.Setup(repo => repo.GetUsuario(usuarioId)).ReturnsAsync((Usuario)null);

        var result = await _controller.GetUsuario(usuarioId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddUsuario_ReturnsCreatedAtAction_WithNewUsuario()
    {
        var newUsuario = new Usuario {};
        _usuarioRepositoryMock.Setup(repo => repo.AddUsuario(newUsuario)).ReturnsAsync(newUsuario);

        var result = await _controller.AddUsuario(newUsuario);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Usuario>(createdAtActionResult.Value);
        Assert.Equal(newUsuario.IdUsuario, returnValue.IdUsuario);
    }

    [Fact]
    public async Task UpdateUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
    {
        int usuarioId = 1;
        var usuarioToUpdate = new Usuario { IdUsuario = usuarioId };
        _usuarioRepositoryMock.Setup(repo => repo.GetUsuario(usuarioId)).ReturnsAsync((Usuario)null);

        var result = await _controller.UpdateUsuario(usuarioId, usuarioToUpdate);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteUsuario_ReturnsOkResult_WhenSuccessfullyDeleted()
    {
        int usuarioId = 1;
        var usuarioToDelete = new Usuario { IdUsuario = usuarioId };
        _usuarioRepositoryMock.Setup(repo => repo.GetUsuario(usuarioId)).ReturnsAsync(usuarioToDelete);
        _usuarioRepositoryMock.Setup(repo => repo.DeleteUsuario(usuarioId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteUsuario(usuarioId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Usuário com id {usuarioId} deletado", okResult.Value);
    }

    [Fact]
    public async Task DeleteUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
    {
        int usuarioId = 1;
        _usuarioRepositoryMock.Setup(repo => repo.GetUsuario(usuarioId)).ReturnsAsync((Usuario)null);

        var result = await _controller.DeleteUsuario(usuarioId);

        Assert.IsType<NotFoundResult>(result);
    }
}
