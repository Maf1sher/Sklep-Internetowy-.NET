namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class OrderViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string UserId { get; set; }
    }
}
