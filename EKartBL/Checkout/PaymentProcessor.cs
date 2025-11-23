using System;

namespace EKartBL
{
    // Responsible only for "charging" the payment
    public class PaymentProcessor
    {
        public void Charge(Order order)
        {
            Console.WriteLine();
            Console.WriteLine("Charging payment of INR " + order.GrandTotal +
                              " for customer " + order.Customer.Name + "...");
            Console.WriteLine("(Pretend payment was successful)");
        }
    }
}
