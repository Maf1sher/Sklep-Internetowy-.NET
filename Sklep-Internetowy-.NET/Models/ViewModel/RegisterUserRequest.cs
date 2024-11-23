using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 50 character allowed")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ,
            ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Max 50 or min 5 characters allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your password")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Max 50 or min 5 characters allowed")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        [MaxLength(20, ErrorMessage = "Max 50 character allowed")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string Address { get; set; }
    }
}
