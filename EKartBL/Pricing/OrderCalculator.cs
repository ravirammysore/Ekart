namespace EKartBL
{
    // Now delegates tax & discount to strategy interfaces (OCP)
    public class OrderCalculator
    {
        private readonly ITaxCalculator _taxCalculator;
        private readonly IDiscountPolicy _discountPolicy;

        public OrderCalculator(ITaxCalculator taxCalculator, IDiscountPolicy discountPolicy)
        {
            _taxCalculator = taxCalculator;
            _discountPolicy = discountPolicy;
        }

        public void CalculateTotals(Order order)
        {
            // 1. Calculate subtotal
            decimal subtotal = 0m;
            foreach (var line in order.OrderLines)
            {
                subtotal += line.Product.UnitPrice * line.Quantity;
            }
            order.SubTotal = subtotal;

            // 2. Discount percentage now comes from policy
            decimal discountPercentage = _discountPolicy.CalculateDiscountPercentage(order);

            order.DiscountAmount = (subtotal * discountPercentage) / 100m;
            decimal amountAfterDiscount = subtotal - order.DiscountAmount;

            // 3. Tax now comes from tax calculator
            order.TaxAmount = _taxCalculator.CalculateTax(amountAfterDiscount, order);

            // 4. Grand total
            order.GrandTotal = amountAfterDiscount + order.TaxAmount;
        }
    }
}
