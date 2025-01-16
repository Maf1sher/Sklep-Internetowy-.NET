namespace Sklep_Internetowy_.NET.Models.Entity;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedData { get; set; } = DateTime.UtcNow;
    public string ImagePath { get; set; }

    public bool IsOnSale { get; set; } = false;
    public decimal? PreviousPrice { get; set; }

    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}