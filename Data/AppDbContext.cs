using LojaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaOnline.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    
    public DbSet<Usuario> Usuarios => Set<Usuario>();
}