using System.ComponentModel.DataAnnotations;

namespace MeusCursos.Models
{
    public class Tutor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do tutor")]
        [MaxLength(60, ErrorMessage = "O nome deve conter até 60 caracteres")]
        public string Name { get; set; }
    }
}