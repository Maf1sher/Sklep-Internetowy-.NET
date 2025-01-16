using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class OrderProduct
    {
        //[Key]
        //public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
