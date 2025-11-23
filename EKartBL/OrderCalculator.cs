namespace EKartBL
{
    // Responsible only for calculating money-related values on the Order
    public class OrderCalculator
    {
        public void CalculateTotals(Order order)
        {
            // 1. Calculate subtotal
            decimal subtotal = 0m;
            foreach (var line in order.OrderLines)
            {
                subtotal += line.Product.UnitPrice * line.Quantity;
            }
            order.SubTotal = subtotal;

            // 2. Apply discount based on loyalty level (still ugly/closed)
            decimal discountPercentage = 0m;
            if (order.Customer.LoyaltyLevel == "Premium")
            {
                discountPercentage = 10m; // 10% discount for premium
            }
            else if (order.Customer.LoyaltyLevel == "Regular")
            {
                discountPercentage = 0m;
            }
            else
            {
                // OCP violation kept for later discussion
                discountPercentage = 0m;
            }

            order.DiscountAmount = (subtotal * discountPercentage) / 100m;
            decimal amountAfterDiscount = subtotal - order.DiscountAmount;

            // 3. Calculate tax (India-only 18%, still hard-coded)
            decimal taxPercentage = 18m;
            order.TaxAmount = (amountAfterDiscount * taxPercentage) / 100m;

            // 4. Grand total
            order.GrandTotal = amountAfterDiscount + order.TaxAmount;
        }
    }
}
