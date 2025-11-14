using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Models;

namespace ProjetoMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : 
            base(options) { }
        public DbSet<Produto> Produtos { get; set; }   
        
        public DbSet<Fornecedor> Fornecedores { get; set; }// novo DbSet para Fornecedor

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Polimorfismo do tipo sobrescrita
        {
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(p => p.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict); //p e f são relacionamentos
        }
    }
}
