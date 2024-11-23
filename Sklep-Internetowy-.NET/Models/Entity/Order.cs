using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedData { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public int? UserId { get; set; }

        //public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
