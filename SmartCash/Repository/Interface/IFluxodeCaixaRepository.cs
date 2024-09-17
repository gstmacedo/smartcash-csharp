using SmartCash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCash.Repository.Interface
{
    public interface IFluxoCaixaRepository
    {
        Task<IEnumerable<FluxoCaixa>> GetFluxoCaixas();
        Task<FluxoCaixa> GetFluxoCaixa(int fluxoCaixaId);
        Task<FluxoCaixa> AddFluxoCaixa(FluxoCaixa fluxoCaixa);
        Task<FluxoCaixa> UpdateFluxoCaixa(FluxoCaixa fluxoCaixa);
        void DeleteFluxoCaixa(int fluxoCaixaId);
    }
}
