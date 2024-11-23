using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 50 character allowed")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        [MaxLength(20, ErrorMessage = "Max 50 character allowed")]
        public string Zip {  get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character allowed")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [MaxLength(50)]
        public string Role { get; set; }

        public List<Order> Ordes { get; set; } = new List<Order>();


    }
}
