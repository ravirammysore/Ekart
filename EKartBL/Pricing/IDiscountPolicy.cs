namespace EKartBL
{
    public interface IDiscountPolicy
    {
        // Returns a percentage (e.g. 10 means 10%)
        decimal CalculateDiscountPercentage(Order order);
    }
}
