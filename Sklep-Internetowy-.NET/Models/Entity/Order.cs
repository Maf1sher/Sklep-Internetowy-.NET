using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedData { get; set; } = DateTime.Now;

        public OrderStatus? Status { get; set; }
        public int? StatusId { get; set; }

        public ShippingMethod? ShippingMethod { get; set; }
        public int? ShippingMethodId { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }
        public int? PaymentMethodId { get; set; }

        public User? User { get; set; }
        public int? UserId { get; set; }

        //public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
