using System.ComponentModel.DataAnnotations;

namespace QQVault.Models.ViewModels
{
    public class UserViewModels
    {
        public class LoginModel
        {
            [UIHint("Student/ID/Staff Number")]
            [Required(ErrorMessage = "Enter your Student/ID/Staff Number.")]
            public string NumberInput { get; set; }

            [UIHint("password")]
            [Required(ErrorMessage = "Enter your password.")]
            public string Password { get; set; }

            public bool RememberMe { get; set; }

            public string ReturnUrl { get; set; } = "/";
        }


        public class RegisterModel
        {
            [Required(ErrorMessage = "Select a user type.")]
            [Display(Name = "User Type")]
            public UserType UserType { get; set; }

            [Required(ErrorMessage = "Enter your Student/ID/Staff Number.")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
            [MaxLength(13, ErrorMessage = "Input cannot exceed 13 digits.")]
            [MinLength(7, ErrorMessage = "Input must be at least 7 digits.")]
            public string NumberInput { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "E-mail")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Enter your cell phone number.")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Cell phone number must be exactly 10 digits.")]
            [Display(Name = "Cell Phone Number")]
            public string CellPhoneNumber { get; set; }

            [Required(ErrorMessage = "Enter your password.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "Passwords must match")]
            [Display(Name = "Confirm Password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }
    }
}
