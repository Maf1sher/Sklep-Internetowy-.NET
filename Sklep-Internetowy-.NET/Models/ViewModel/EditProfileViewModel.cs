using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]
        public string Zip { get; set; }
    }
}
