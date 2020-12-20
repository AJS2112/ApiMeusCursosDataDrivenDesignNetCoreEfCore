using System.ComponentModel.DataAnnotations;

namespace MeusCursos.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é requerido")]
        [MaxLength(40, ErrorMessage = "Contem até 40 caracteres no maximo")]
        public string Name { get; set; }
        public string Url { get; set; }
    }
}