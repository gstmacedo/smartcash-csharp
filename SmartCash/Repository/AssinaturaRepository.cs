using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository
{
    public class AssinaturaRepository
    {
        private readonly dbContext dbContext;

        public AssinaturaRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Assinatura> AddAssinatura(Assinatura assinatura)
        {
            var result = await dbContext.Assinaturas.AddAsync(assinatura);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAssinatura(long assinaturaId) // Ajustado para long
        {
            var result = await dbContext.Assinaturas.FirstOrDefaultAsync(x => x.IdAssinatura == assinaturaId);
            if (result != null)
            {
                dbContext.Assinaturas.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Assinatura> GetAssinatura(long assinaturaId) // Ajustado para long
        {
            return await dbContext.Assinaturas.FirstOrDefaultAsync(x => x.IdAssinatura == assinaturaId);
        }

        public async Task<IEnumerable<Assinatura>> GetAssinaturas()
        {
            return await dbContext.Assinaturas.ToListAsync();
        }

        public async Task<Assinatura> UpdateAssinatura(Assinatura assinatura)
        {
            var result = await dbContext.Assinaturas.FirstOrDefaultAsync(x => x.IdAssinatura == assinatura.IdAssinatura);
            if (result != null)
            {
                result.Tipo = assinatura.Tipo; // Ajustado para os campos da model Assinatura
                result.Valor = assinatura.Valor;

                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
