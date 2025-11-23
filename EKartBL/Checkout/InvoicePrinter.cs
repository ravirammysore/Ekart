using System;

namespace EKartBL
{
    // Responsible only for printing the invoice
    public class InvoicePrinter
    {
        public void Print(Order order)
        {
            Console.WriteLine();
            Console.WriteLine("========= EKart Invoice =========");
            Console.WriteLine("Order Id   : " + order.Id);
            Console.WriteLine("Customer   : " + order.Customer.Name +
                              " (" + order.Customer.LoyaltyLevel + ")");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Items:");
            foreach (var line in order.OrderLines)
            {
                decimal lineTotal = line.Product.UnitPrice * line.Quantity;
                Console.WriteLine(
                    $" - {line.Product.Name} x {line.Quantity} @ {line.Product.UnitPrice} = {lineTotal}");
            }
            Console.WriteLine("---------------------------------");
            Console.WriteLine("SubTotal       : " + order.SubTotal);
            Console.WriteLine("Discount Amount: " + order.DiscountAmount);
            Console.WriteLine("Tax Amount     : " + order.TaxAmount);
            Console.WriteLine("Grand Total    : " + order.GrandTotal);
            Console.WriteLine("=================================");
        }
    }
}
