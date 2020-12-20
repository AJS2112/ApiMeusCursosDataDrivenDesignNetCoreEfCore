using System.ComponentModel.DataAnnotations;

namespace MeusCursos.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Infome a senha")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}