using System.ComponentModel.DataAnnotations;

namespace SmartCash.Models
{
    public class Usuario
    {
        [Key]
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"(\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2})|(\d{3}\.\d{3}\.\d{3}-\d{2})", ErrorMessage = "Documento (CPF/CNPJ) inválido.")]
        public string Documento { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Senha { get; set; }
    }
}
