using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using SmartCash.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class FluxoCaixaRepositoryTests
{
    private readonly FluxoCaixaRepository _repository;
    private readonly dbContext _context;

    public FluxoCaixaRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<dbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new dbContext(options);
        _repository = new FluxoCaixaRepository(_context);
    }

    [Fact]
    public async Task AddFluxoCaixa_AddsFluxoCaixa()
    {
        var fluxoCaixa = new FluxoCaixa { IdFluxo = 1, Valor = 100.0m, Descricao = "Entrada", Tipo = "Receita" };

        var result = await _repository.AddFluxoCaixa(fluxoCaixa);

        Assert.NotNull(result);
        Assert.Equal(100.0m, result.Valor);
        Assert.Equal("Entrada", result.Descricao);
    }

    [Fact]
    public async Task GetFluxoCaixa_ReturnsFluxoCaixa()
    {
        var fluxoCaixa = new FluxoCaixa { IdFluxo = 1, Valor = 100.0m, Descricao = "Entrada", Tipo = "Receita" };
        await _repository.AddFluxoCaixa(fluxoCaixa);

        var result = await _repository.GetFluxoCaixa(1);

        Assert.NotNull(result);
        Assert.Equal("Entrada", result.Descricao);
    }

    [Fact]
    public async Task UpdateFluxoCaixa_UpdatesFluxoCaixa()
    {
        var fluxoCaixa = new FluxoCaixa { IdFluxo = 1, Valor = 100.0m, Descricao = "Entrada", Tipo = "Receita" };
        await _repository.AddFluxoCaixa(fluxoCaixa);

        fluxoCaixa.Descricao = "Entrada Atualizada";

        var result = await _repository.UpdateFluxoCaixa(fluxoCaixa);

        Assert.NotNull(result);
        Assert.Equal("Entrada Atualizada", result.Descricao);
    }

    [Fact]
    public async Task DeleteFluxoCaixa_RemovesFluxoCaixa()
    {
        var fluxoCaixa = new FluxoCaixa { IdFluxo = 1, Valor = 100.0m, Descricao = "Entrada", Tipo = "Receita" };
        await _repository.AddFluxoCaixa(fluxoCaixa);

        await _repository.DeleteFluxoCaixa(1);

        var result = await _repository.GetFluxoCaixa(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetFluxoCaixas_ReturnsAllFluxoCaixas()
    {
        var fluxoCaixa1 = new FluxoCaixa { IdFluxo = 1, Valor = 100.0m, Descricao = "Entrada 1", Tipo = "Receita" };
        var fluxoCaixa2 = new FluxoCaixa { IdFluxo = 2, Valor = 50.0m, Descricao = "Entrada 2", Tipo = "Receita" };

        await _repository.AddFluxoCaixa(fluxoCaixa1);
        await _repository.AddFluxoCaixa(fluxoCaixa2);

        var result = await _repository.GetFluxoCaixas();

        Assert.Equal(2, result.Count());
    }
}
