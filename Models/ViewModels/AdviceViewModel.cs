namespace QQVault.Models.ViewModels
{
    public class AdviceViewModel
    {
        public string ConsultantName { get; set; }
        public string Message { get; set; } // Update this if your Advice class uses "Message" instead of "AdviceMessage"
        public DateTime CreatedAt { get; set; }
        public string UserId { get; internal set; }
    }
}
