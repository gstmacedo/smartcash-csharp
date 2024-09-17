using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository
{
    public class UsuarioEmpresaRepository
    {
        private readonly dbContext dbContext;

        public UsuarioEmpresaRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UsuarioEmpresa> AddUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa)
        {
            var result = await dbContext.UsuarioEmpresas.AddAsync(usuarioEmpresa);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUsuarioEmpresa(int usuarioEmpresaId)
        {
            var result = await dbContext.UsuarioEmpresas.FirstOrDefaultAsync(x => x.IdUsuarioEmpresa == usuarioEmpresaId);
            if (result != null)
            {
                dbContext.UsuarioEmpresas.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<UsuarioEmpresa> GetUsuarioEmpresa(int usuarioEmpresaId)
        {
            return await dbContext.UsuarioEmpresas.FirstOrDefaultAsync(x => x.IdUsuarioEmpresa == usuarioEmpresaId);
        }

        public async Task<IEnumerable<UsuarioEmpresa>> GetUsuarioEmpresas()
        {
            return await dbContext.UsuarioEmpresas.ToListAsync();
        }

        public async Task<UsuarioEmpresa> UpdateUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa)
        {
            var result = await dbContext.UsuarioEmpresas.FirstOrDefaultAsync(x => x.IdUsuarioEmpresa == usuarioEmpresa.IdUsuarioEmpresa );
            if (result != null)
            {
                result.Empresa = usuarioEmpresa.Empresa;
                result.Usuario = usuarioEmpresa.Usuario;
                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
