using System.ComponentModel.DataAnnotations;

namespace SmartCash.Models
{
    public class UsuarioEmpresa
    {
        [Key]
        public long IdUsuarioEmpresa { get; set; }

        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
