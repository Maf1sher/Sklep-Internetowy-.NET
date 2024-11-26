namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class UpdateCartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
