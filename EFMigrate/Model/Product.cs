namespace EFMigrate.Model;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}