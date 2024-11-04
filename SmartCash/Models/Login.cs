using System.ComponentModel.DataAnnotations;

namespace SmartCash.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Senha { get; set; }
    }
}
