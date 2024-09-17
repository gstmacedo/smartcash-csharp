using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuario(int usuarioId);
        Task<Usuario> AddUsuario(Usuario usuario);
        Task<Usuario> UpdateUsuario(Usuario usuario);
        void DeleteUsuario(int usuarioId);
    }
}
