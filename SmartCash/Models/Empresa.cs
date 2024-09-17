using System.ComponentModel.DataAnnotations;

namespace SmartCash.Models
{
    public class Empresa
    {
        [Key]
        public long IdEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}", ErrorMessage = "CNPJ inválido.")]
        public string Cnpj { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Ramo { get; set; }
    }
}
