using MeusCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace MeusCursos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Technology> Techonologies { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<User> Users { get; set; }

    }
}