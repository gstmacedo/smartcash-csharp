using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;
using SmartCash.Models;

public class UsuarioEmpresaTests
{
    [Fact]
    public void UsuarioEmpresa_ValidModel_ShouldBeValid()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            Nome = "Test User",
            Documento = "123.456.789-00",
            Email = "testuser@example.com",
            SenhaHash = "hashedPassword"
        };

        var empresa = new Empresa
        {
            IdEmpresa = 1,
            Nome = "TestCompany"
        };

        var usuarioEmpresa = new UsuarioEmpresa
        {
            IdUsuarioEmpresa = 1,
            Usuario = usuario,
            Empresa = empresa
        };

        var validationResults = ValidateModel(usuarioEmpresa);

        Assert.Empty(validationResults);
    }

    [Fact]
    public void UsuarioEmpresa_MissingUsuario_ShouldBeInvalid()
    {
        var empresa = new Empresa
        {
            IdEmpresa = 1,
            Nome = "TestCompany"
        };

        var usuarioEmpresa = new UsuarioEmpresa
        {
            IdUsuarioEmpresa = 1,
            Usuario = null,
            Empresa = empresa
        };

        var validationResults = ValidateModel(usuarioEmpresa);

        Assert.Single(validationResults);
        Assert.Equal("The Usuario field is required.", validationResults.First().ErrorMessage);
    }

    [Fact]
    public void UsuarioEmpresa_MissingEmpresa_ShouldBeInvalid()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            Nome = "Test User",
            Documento = "123.456.789-00",
            Email = "testuser@example.com",
            SenhaHash = "hashedPassword"
        };

        var usuarioEmpresa = new UsuarioEmpresa
        {
            IdUsuarioEmpresa = 1,
            Usuario = usuario,
            Empresa = null
        };

        var validationResults = ValidateModel(usuarioEmpresa);

        Assert.Single(validationResults);
        Assert.Equal("The Empresa field is required.", validationResults.First().ErrorMessage);
    }

    private IList<ValidationResult> ValidateModel(UsuarioEmpresa usuarioEmpresa)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(usuarioEmpresa);
        Validator.TryValidateObject(usuarioEmpresa, validationContext, validationResults, true);
        return validationResults;
    }
}
