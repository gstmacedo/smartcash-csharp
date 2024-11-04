using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using SmartCash.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class EmpresaRepositoryTests
{
    private readonly EmpresaRepository _repository;
    private readonly DbContextOptions<dbContext> _options;

    public EmpresaRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<dbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        var context = new dbContext(_options);
        _repository = new EmpresaRepository(context);
    }

    [Fact]
    public async Task AddEmpresa_AddsEmpresa()
    {
        var empresa = new Empresa { IdEmpresa = 1, Nome = "Empresa Teste", Cnpj = "12345678000195", Email = "contato@empresa.com", Ramo = "Serviços" };

        var result = await _repository.AddEmpresa(empresa);

        Assert.NotNull(result);
        Assert.Equal("Empresa Teste", result.Nome);
        Assert.Equal(1, result.IdEmpresa);
    }

    [Fact]
    public async Task GetEmpresa_ReturnsEmpresa()
    {
        var empresa = new Empresa { IdEmpresa = 1, Nome = "Empresa Teste", Cnpj = "12345678000195", Email = "contato@empresa.com", Ramo = "Serviços" };
        await _repository.AddEmpresa(empresa);

        var result = await _repository.GetEmpresa(1);

        Assert.NotNull(result);
        Assert.Equal("Empresa Teste", result.Nome);
    }

    [Fact]
    public async Task UpdateEmpresa_UpdatesEmpresa()
    {
        var empresa = new Empresa { IdEmpresa = 1, Nome = "Empresa Teste", Cnpj = "12345678000195", Email = "contato@empresa.com", Ramo = "Serviços" };
        await _repository.AddEmpresa(empresa);

        empresa.Nome = "Empresa Atualizada";

        var result = await _repository.UpdateEmpresa(empresa);

        Assert.NotNull(result);
        Assert.Equal("Empresa Atualizada", result.Nome);
    }

    [Fact]
    public async Task DeleteEmpresa_RemovesEmpresa()
    {
        var empresa = new Empresa { IdEmpresa = 1, Nome = "Empresa Teste", Cnpj = "12345678000195", Email = "contato@empresa.com", Ramo = "Serviços" };
        await _repository.AddEmpresa(empresa);

        await _repository.DeleteEmpresa(1);

        var result = await _repository.GetEmpresa(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEmpresas_ReturnsAllEmpresas()
    {
        var empresa1 = new Empresa { IdEmpresa = 1, Nome = "Empresa Teste 1", Cnpj = "12345678000195", Email = "contato1@empresa.com", Ramo = "Serviços" };
        var empresa2 = new Empresa { IdEmpresa = 2, Nome = "Empresa Teste 2", Cnpj = "12345678000196", Email = "contato2@empresa.com", Ramo = "Produtos" };

        await _repository.AddEmpresa(empresa1);
        await _repository.AddEmpresa(empresa2);

        var result = await _repository.GetEmpresas();

        Assert.Equal(2, result.Count());
    }
}
