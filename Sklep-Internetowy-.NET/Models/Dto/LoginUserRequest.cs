using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Models.Dto
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 50 character allowed")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Max 50 or min 5 characters allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
