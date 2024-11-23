namespace Sklep_Internetowy_.NET.Models.Dto
{
    public class AddProductRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
