using Microsoft.EntityFrameworkCore;
using TesteAPI1.Models;

namespace TesteAPI1.Data
{
    public class TesteApiContext : DbContext
    {
        public TesteApiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}