using _3dPrinter.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace _3dPrinter.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Filament> Filament { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
