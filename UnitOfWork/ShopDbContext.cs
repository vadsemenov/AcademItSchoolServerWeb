using UnitOfWork.Model;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork;

public class ShopDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    private const string ConnectionString = "Data Source=.;" +
                                            "Initial Catalog=UOWEFShop;" +
                                            "Integrated Security=true;" +
                                            "TrustServerCertificate=true";
    public ShopDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(builder =>
            builder.Property(category => category.Name)
                .HasMaxLength(60)
                .IsRequired()
            );

        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasMany(product => product.Categories)
                .WithMany(category => category.Products)
                .UsingEntity(productCategory => productCategory.ToTable("ProductsCategories"));

            builder.HasMany(product => product.OrderItems)
                .WithOne(orderItem => orderItem.Product);


            builder.Property(product => product.Name)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(product => product.Price)
                .HasMaxLength(15)
                .HasPrecision(10, 2)
                .HasDefaultValue(0.00)
                .IsRequired();

        });

        modelBuilder.Entity<Customer>(builder =>
        {
            builder.Property(customer => customer.FirstName)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(customer => customer.MiddleName)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(customer => customer.LastName)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(customer => customer.PhoneNumber)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(customer => customer.Email)
                .HasMaxLength(60)
                .IsRequired();
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders);

            builder.HasMany(order => order.OrderItems)
                .WithOne(orderItem => orderItem.Order);

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

            builder.Property(orderItem => orderItem.Count)
                .HasMaxLength(10)
                .IsRequired();
        });
    }
}