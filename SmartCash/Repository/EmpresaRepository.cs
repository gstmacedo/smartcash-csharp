using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository
{
    public class EmpresaRepository
    {
        private readonly dbContext dbContext;

        public EmpresaRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Empresa> AddEmpresa(Empresa empresa)
        {
            var result = await dbContext.Empresas.AddAsync(empresa);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteEmpresa(int empresaId)
        {
            var result = await dbContext.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == empresaId);
            if (result != null)
            {
                dbContext.Empresas.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Empresa> GetEmpresa(int empresaId)
        {
            return await dbContext.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == empresaId);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresas()
        {
            return await dbContext.Empresas.ToListAsync();
        }

        public async Task<Empresa> UpdateEmpresa(Empresa empresa)
        {
            var result = await dbContext.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == empresa.IdEmpresa);
            if (result != null)
            {
                result.Nome = empresa.Nome;
                result.Cnpj = empresa.Cnpj;
                result.Email = empresa.Email;
                result.Ramo = empresa.Ramo;
                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
