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
        public DbSet<Order> Orders { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasMany(w => w.Ordes)
                .WithOne(c => c.User)
                .HasForeignKey(w => w.UserId);
            });

            //modelBuilder.Entity<OrderProduct>()
            //    .HasKey(c => new { c.OrderId, c.ProductId });

            modelBuilder.Entity<Order>(eb =>
            {
                eb.HasMany(w => w.Products)
                .WithMany(t => t.Orders);
            });

        }

    }
}
