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
    }
}
