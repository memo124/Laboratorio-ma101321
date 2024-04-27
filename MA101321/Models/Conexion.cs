using Microsoft.EntityFrameworkCore;

namespace MA101321.Models
{
    public class Conexion: DbContext
    {
        public DbSet<autor> autor { get; set; }
        public DbSet<libro> libro { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-O8SAEK9T;database=ma101321;user=sa;password=sa;Encrypt=true;TrustServerCertificate=true;Integrated Security=false;MultipleActiveResultSets=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<autor>().ToTable("autor", schema: "dbo");
            modelBuilder.Entity<libro>().ToTable("libro", schema: "dbo");
        }
    }
}
