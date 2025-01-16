namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class UserOrdersViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public List<UserOrderItemsViewModel> OrderItems { get; set; }
    }
}
