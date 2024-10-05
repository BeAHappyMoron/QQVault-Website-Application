using System;
using System.ComponentModel.DataAnnotations;

namespace QQVault.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }

        public string UserId { get; set; } // Link to the User
        public DateTime CreatedAt { get; set; }
        public int Rating { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
