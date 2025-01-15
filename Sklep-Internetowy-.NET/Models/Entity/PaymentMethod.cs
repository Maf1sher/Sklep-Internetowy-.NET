using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        public string MethodName { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
