namespace EKartApp
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        // "Regular" or "Premium" for now
        public string LoyaltyLevel { get; set; } = "Regular";
    }
}
