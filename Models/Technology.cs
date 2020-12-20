using System.ComponentModel.DataAnnotations;

namespace MeusCursos.Models
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(60, ErrorMessage = "Deve conter no maximo 60 caracteres")]
        public string Name { get; set; }
    }
}