using System;

namespace QQVault.Models
{
    public class Advice
    {
        public int Id { get; set; }
        public string ConsultantName { get; set; } // The name of the consultant giving advice
        public string Message { get; set; } // Assuming this is the message containing the advice
        public DateTime CreatedAt { get; set; } // Date and time when advice was created

        // Add the UserId property to link the advice to a specific user
        public string UserId { get; set; } // Foreign key to the User model

        // Navigation property to the User (if needed)
        public User User { get; set; }
    }
}
