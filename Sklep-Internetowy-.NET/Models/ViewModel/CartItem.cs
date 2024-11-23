using Sklep_Internetowy_.NET.Models.Entity;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
