using System;
using System.Collections.Generic;

namespace EKartApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EKart Checkout Demo (Stage 0 - Ugly but Working) ===");

            // Create sample data
            var customer = new Customer
            {
                Id = 1,
                Name = "Ravi Ram",
                LoyaltyLevel = "Premium" // or "Regular"
            };

            var repo = new EkartRepository();

            // Simulate products stored somewhere
            repo.SeedProducts();

            // Build order
            var order = new Order
            {
                Id = 1001,
                Customer = customer,
                OrderLines = new List<OrderLine>()
            };

            // Customer buys 2 units of Product 1 and 1 unit of Product 2
            var product1 = repo.GetProductById(1);
            var product2 = repo.GetProductById(2);

            order.OrderLines.Add(new OrderLine
            {
                Product = product1,
                Quantity = 2
            });

            order.OrderLines.Add(new OrderLine
            {
                Product = product2,
                Quantity = 1
            });

            var checkoutService = new CheckoutService(repo);
            checkoutService.ProcessOrder(order);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
