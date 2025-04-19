namespace MusicalInstrumentShop.ViewModels
{
    public class RazorpayCheckoutViewModel
    {
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageLogo { get; set; } = string.Empty;
        public string RazorpayKey { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        // Helper property to get amount in paisa (Razorpay expects amount in paisa)
        public int AmountInPaisa => (int)(Amount * 100m);
    }
}