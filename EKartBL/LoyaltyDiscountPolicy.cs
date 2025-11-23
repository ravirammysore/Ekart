namespace EKartBL
{
    // Discount based only on LoyaltyLevel: Premium = 10%, others = 0%
    public class LoyaltyDiscountPolicy : IDiscountPolicy
    {
        public decimal CalculateDiscountPercentage(Order order)
        {
            if (order.Customer.LoyaltyLevel == "Premium")
            {
                return 10m;
            }

            if (order.Customer.LoyaltyLevel == "Regular")
            {
                return 0m;
            }

            // Any unknown loyalty level gets 0% by default
            return 0m;
        }
    }
}
