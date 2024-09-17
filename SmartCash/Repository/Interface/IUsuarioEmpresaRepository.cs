using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository.Interface
{
    public interface IUsuarioEmpresaRepository
    {
        Task<IEnumerable<UsuarioEmpresa>> GetUsuarioEmpresas();
        Task<UsuarioEmpresa> GetUsuarioEmpresa(int usuarioEmpresaId);
        Task<UsuarioEmpresa> AddUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa);
        Task<UsuarioEmpresa> UpdateUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa);
        void DeleteUsuarioEmpresa(int usuarioEmpresaId);
    }
}
