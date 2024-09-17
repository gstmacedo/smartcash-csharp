using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository.Interface
{
    public interface IAssinaturaRepository
    {
        Task<IEnumerable<Assinatura>> GetAssinaturas();
        Task<Assinatura> GetAssinatura(int assinaturaId);
        Task<Assinatura> AddAssinatura(Assinatura assinatura);
        Task<Assinatura> UpdateAssinatura(Assinatura assinatura);
        void DeleteAssinatura(int assinaturaId);
    }
}
