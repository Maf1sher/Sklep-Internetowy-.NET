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
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasMany(w => w.Ordes)
                .WithOne(c => c.User)
                .HasForeignKey(w => w.UserId);
            });

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

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

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict
            );

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade
            );

            modelBuilder.Entity<OrderStatus>().HasData(
                    new OrderStatus { Id = 1, StatusName = "Nowe" },
                    new OrderStatus { Id = 2, StatusName = "W trakcie realizacji" },
                    new OrderStatus { Id = 3, StatusName = "Zrealizowane" },
                    new OrderStatus { Id = 4, StatusName = "Anulowane" }
                );

            modelBuilder.Entity<PaymentMethod>().HasData(
                    new PaymentMethod { Id = 1, MethodName = "Blik" },
                    new PaymentMethod { Id = 2, MethodName = "Przelew bankowy" },
                    new PaymentMethod { Id = 3, MethodName = "PayU" }
                );

            modelBuilder.Entity<ShippingMethod>().HasData(
                    new ShippingMethod { Id = 1, ShippingName = "InPost" },
                    new ShippingMethod { Id = 2, ShippingName = "DHL" },
                    new ShippingMethod { Id = 3, ShippingName = "Odbiór osobisty" }
                );
        }
    }
}
