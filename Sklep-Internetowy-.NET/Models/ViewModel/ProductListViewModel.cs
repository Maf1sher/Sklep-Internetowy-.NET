using Sklep_Internetowy_.NET.Models.Entity;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
	public class ProductListViewModel
	{
		public List<Product> AllProducts { get; set; }
		public List<Product> NewProducts { get; set; }
        public List<Product> OnSaleProducts { get; set; }
    }
}
