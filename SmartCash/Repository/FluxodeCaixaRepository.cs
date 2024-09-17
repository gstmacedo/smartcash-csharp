using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository
{
    public class FluxoCaixaRepository
    {
        private readonly dbContext dbContext;

        public FluxoCaixaRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<FluxoCaixa> AddFluxoCaixa(FluxoCaixa fluxoCaixa)
        {
            var result = await dbContext.FluxoCaixas.AddAsync(fluxoCaixa);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteFluxoCaixa(int fluxoCaixaId)
        {
            var result = await dbContext.FluxoCaixas.FirstOrDefaultAsync(x => x.IdFluxo == fluxoCaixaId);
            if (result != null)
            {
                dbContext.FluxoCaixas.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<FluxoCaixa> GetFluxoCaixa(int fluxoCaixaId)
        {
            return await dbContext.FluxoCaixas.FirstOrDefaultAsync(x => x.IdFluxo == fluxoCaixaId);
        }

        public async Task<IEnumerable<FluxoCaixa>> GetFluxoCaixas()
        {
            return await dbContext.FluxoCaixas.ToListAsync();
        }

        public async Task<FluxoCaixa> UpdateFluxoCaixa(FluxoCaixa fluxoCaixa)
        {
            var result = await dbContext.FluxoCaixas.FirstOrDefaultAsync(x => x.IdFluxo == fluxoCaixa.IdFluxo);
            if (result != null)
            {
                result.Valor = fluxoCaixa.Valor;
                result.Descricao = fluxoCaixa.Descricao;
                result.Tipo = fluxoCaixa.Tipo;
                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
