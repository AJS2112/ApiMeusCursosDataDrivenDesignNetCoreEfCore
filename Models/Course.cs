using System;
using System.ComponentModel.DataAnnotations;
using MeusCursos.Model.Enums;
using MeusCursos.Models.Enums;

namespace MeusCursos.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o titulo do curso")]
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Hours { get; set; }
        public double Price { get; set; }
        public int IdCategory { get; set; }
        public int IdTechnology { get; set; }
        public int IdPlatform { get; set; }
        public int IdTutor { get; set; }

        public Nivel Nivel { get; set; }
        public Status Status { get; set; }
        public Technology Technology { get; set; }
        public Category Category { get; set; }
        public Tutor Tutor { get; set; }
        public Platform Platform { get; set; }
    }
}