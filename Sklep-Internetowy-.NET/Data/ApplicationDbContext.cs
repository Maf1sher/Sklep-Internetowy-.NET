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
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        

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

            modelBuilder.Entity<PaymentMethod>(eb =>
            {
                eb.HasMany(w => w.Orders)
                .WithOne(t => t.PaymentMethod)
                .HasForeignKey(c => c.PaymentMethodId);
            });

            modelBuilder.Entity<ShippingMethod>(eb =>
            {
                eb.HasMany(w => w.Orders)
                .WithOne(t => t.ShippingMethod)
                .HasForeignKey(c => c.ShippingMethodId);
            });

            modelBuilder.Entity<OrderStatus>(eb =>
            {
                eb.HasMany(w => w.Orders)
                .WithOne(t => t.Status)
                .HasForeignKey(c => c.StatusId);
            });
        }
    }
}
