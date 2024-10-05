using QQVault.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QQVault.Models.ViewModels
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Enter a username.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and digits.")]
        public string Username { get; set; }

        public UserType UserType { get; set; }

        [Required(ErrorMessage = "Enter your Student/ID/Staff Number.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [MaxLength(13, ErrorMessage = "Input cannot exceed 13 digits.")]
        [MinLength(7, ErrorMessage = "Input must be at least 7 digits.")]
        public string NumberInput { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your cell phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Cell phone number must be 10 digits.")]
        public string CellPhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public UserRole SelectedRole { get; set; }

        public bool IsValidNumberInput()
        {
            switch (UserType)
            {
                case UserType.Student:
                    return NumberInput.Length == 10; // Validate student number length
                case UserType.Staff:
                    return NumberInput.Length == 7; // Validate staff number length
                case UserType.Guest:
                    return NumberInput.Length == 13; // Validate ID number length
                default:
                    return false;
            }
        }
    }
}
