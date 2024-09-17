using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository.Interface
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetEmpresas();
        Task<Empresa> GetEmpresa(int empresaId);
        Task<Empresa> AddEmpresa(Empresa empresa);
        Task<Empresa> UpdateEmpresa(Empresa empresa);
        void DeleteEmpresa(int empresaId);
    }
}

