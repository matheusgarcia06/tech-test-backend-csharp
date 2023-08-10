using Microsoft.EntityFrameworkCore;
using TechTestBackendCSharp.Models;

namespace TechTestBackendCSharp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
