using EFShop.Model;
using Microsoft.EntityFrameworkCore;

namespace EFShop;

public class ShopDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    private const string ConnectionString = "Data Source=.;" +
                                            "Initial Catalog=EFShop;" +
                                            "Integrated Security=true;" +
                                            "TrustServerCertificate=true";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(builder =>
        {
            builder.Property(category => category.Name)
                .HasMaxLength(60);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasMany(product => product.Categories)
                .WithMany(category => category.Products)
                .UsingEntity(
                    r => r
                        .HasOne(typeof(Category))
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .HasPrincipalKey(nameof(Category.Id)),
                    l => l
                        .HasOne(typeof(Product))
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .HasPrincipalKey(nameof(Product.Id)),
                    productCategory => productCategory
                        .ToTable("ProductsCategories"));

            builder.Property(product => product.Name)
                .HasMaxLength(60);

            builder.Property(product => product.Price)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<Customer>(builder =>
        {
            builder.Property(customer => customer.FirstName)
                .HasMaxLength(60);

            builder.Property(customer => customer.MiddleName)
                .HasMaxLength(60);

            builder.Property(customer => customer.LastName)
                .HasMaxLength(60);

            builder.Property(customer => customer.PhoneNumber)
                .HasMaxLength(60);

            builder.Property(customer => customer.Email)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders);

            builder.Property(order => order.OrderDate)
                .HasColumnType("date");
        });

        modelBuilder.Entity<OrderItem>(builder =>
        {
            builder.HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

            builder.HasOne(orderItem => orderItem.Product)
                .WithMany(product => product.OrderItems)
                .HasForeignKey(orderItem => orderItem.ProductId);

            builder.Property(orderItem => orderItem.Count);
        });
    }
}