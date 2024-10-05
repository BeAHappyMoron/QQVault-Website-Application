using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace QQVault.Models
{
    public class User : IdentityUser
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Enter a first name.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Enter a last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public override string Email { get; set; }

        [DisplayName("Cell Phone Number")]
        [Required(ErrorMessage = "Enter your cell phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Cell phone number must be 10 digits.")]
        public string CellPhoneNumber { get; set; }

        [DisplayName("Date")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Select a user type.")]
        public UserType UserType { get; set; }

        [DisplayName("Student/ID/Staff Number")]
        [Required(ErrorMessage = "Enter your Student/ID/Staff Number.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [MaxLength(13, ErrorMessage = "Input cannot exceed 13 digits.")]
        [MinLength(7, ErrorMessage = "Input must be at least 7 digits.")]
        public string NumberInput { get; set; }

        // Navigation property for related BankAccounts
        public ICollection<Advice> Advices { get; set; } = new List<Advice>();
        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<Feedback> FeedbackResponses { get; set; } // New property

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

    public enum UserType
    {
        Student,
        Staff,
        Guest
    }

    public enum UserRole
    {
        Administrator,
        Consultant,
        FinancialAdviser,
        Client
    }
}
