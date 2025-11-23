namespace EKartBL
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal amountAfterDiscount, Order order);
    }
}
