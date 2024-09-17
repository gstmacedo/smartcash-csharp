using System.ComponentModel.DataAnnotations;

namespace SmartCash.Models
{
    public class Assinatura
    {
        [Key]
        public long IdAssinatura { get; set; }

        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Valor { get; set; }
    }
}
