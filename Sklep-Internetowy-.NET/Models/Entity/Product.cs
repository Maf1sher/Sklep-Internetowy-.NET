using System.Text.Json.Serialization;

namespace Sklep_Internetowy_.NET.Models.Entity;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    [JsonIgnore]
    public DateTime CreatedData { get; set; } = DateTime.UtcNow;
    public string ImagePath { get; set; }
    [JsonIgnore]
    public Guid CategoryId { get; set; }
    [JsonIgnore]
    public Category Category { get; set; }
    [JsonIgnore]
    public bool IsOnSale { get; set; } = false;
    [JsonIgnore]
    public decimal? PreviousPrice { get; set; }
    [JsonIgnore]
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}