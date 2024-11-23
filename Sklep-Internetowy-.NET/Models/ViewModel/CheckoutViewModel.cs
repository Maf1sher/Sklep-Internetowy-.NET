using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}
