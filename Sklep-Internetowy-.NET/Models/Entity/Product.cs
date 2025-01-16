namespace Sklep_Internetowy_.NET.Models.Entity;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    //public List<Order> Orders { get; set; } = new List<Order>();
}