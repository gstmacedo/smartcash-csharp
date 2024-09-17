using Microsoft.EntityFrameworkCore;
using SmartCash.Models;

namespace SmartCash.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<FluxoCaixa> FluxoCaixas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresas { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Documento)
                .IsUnique()
                .HasDatabaseName("IX_Usuario_Documento"); 

            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.Nome)
                .IsUnique()
                .HasDatabaseName("IX_Empresa_Nome"); 

            modelBuilder.Entity<Assinatura>() 
                .HasIndex(a => a.Tipo)
                .IsUnique()
                .HasDatabaseName("IX_Assinatura_Nome");

            base.OnModelCreating(modelBuilder);
        }
    }
}
