using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class ShippingMethod
    {
        [Key]
        public int Id { get; set; }
        public string ShippingName { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
