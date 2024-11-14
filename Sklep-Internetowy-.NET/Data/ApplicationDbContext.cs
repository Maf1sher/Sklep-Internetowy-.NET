using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Models.Entity;

namespace test_do_projektu.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
