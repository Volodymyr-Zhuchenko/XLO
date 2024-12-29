using ItProger.Models.Category;
using Microsoft.EntityFrameworkCore;
namespace ItProger.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}
