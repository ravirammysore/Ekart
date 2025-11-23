namespace EKartBL
{
    // Simple tax calculator for India (18% flat)
    public class IndiaGstTaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(decimal amountAfterDiscount, Order order)
        {
            decimal taxPercentage = 18m;
            return (amountAfterDiscount * taxPercentage) / 100m;
        }
    }
}
