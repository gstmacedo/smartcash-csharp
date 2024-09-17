using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCash.Models
{
    public class FluxoCaixa
    {
        [Key]
        public long IdFluxo { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Valor { get; set; }

        [Required]
        [StringLength(250)]
        public string Descricao { get; set; }

        public DateTime DataInclusao { get; set; } = DateTime.Now;

        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
