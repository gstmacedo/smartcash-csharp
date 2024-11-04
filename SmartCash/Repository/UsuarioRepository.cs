using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartCash.Data;
using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository
{
    public class UsuarioRepository
    {
        private readonly dbContext dbContext;

        public UsuarioRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Usuario> AddUsuario(Usuario usuario)
        {
            var result = await dbContext.Usuarios.AddAsync(usuario);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUsuario(int usuarioId)
        {
            var result = await dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
            if (result != null)
            {
                dbContext.Usuarios.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Usuario> GetUsuario(int usuarioId)
        {
            return await dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await dbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> UpdateUsuario(Usuario usuario)
        {
            var result = await dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuario.IdUsuario);
            if (result != null)
            {
                result.Nome = usuario.Nome;
                result.Email = usuario.Email;
                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
