using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
