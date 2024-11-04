using Microsoft.AspNetCore.Identity;
using SmartCash.Models;
using SmartCash.Data;
using SmartCash.Utils;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartCash.Services
{
        public class AuthService
        {
            private readonly dbContext _dbContext;
            private readonly PasswordHasher<Usuario> _passwordHasherService;

            public AuthService(dbContext dbContext)
            {
                _dbContext = dbContext;
                _passwordHasherService = new PasswordHasher<Usuario>();
            }

            public async Task<Usuario> Authenticate(string email, string senha)
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
                if (usuario == null)
                {
                    return null;
                }

                var verificationResult = _passwordHasherService.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);
                if (verificationResult != PasswordVerificationResult.Success)
                {
                    return null;
                }

                return usuario;
            }
        }
}
