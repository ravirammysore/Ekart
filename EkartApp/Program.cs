using System;
using System.Collections.Generic;
using EKartBL;

namespace EKartApp
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("=== EKart Checkout Demo (Stage 6 - ISP: repository vs logger split) ===");

            var customer = new Customer
            {
                Id = 1,
                Name = "Ravi Ram",
                LoyaltyLevel = "Premium"
            };

            /*Using the interface type now*
              
            In case we want to switch to a different repository implementation later, this will be the only change needed in the entire application!

            Example: 
                IEkartRepository repo = new SqlEkartRepository();
            or
                IEkartRepository repo = new OrcaleEkartRepository();
            */

            IEkartRepository repo = new EkartRepository();
            repo.SeedProducts();

            /*Similarly, we can swap different logger implementations later if needed, like log to a cloudservice or slack or something!, this will be the only place to change that in the entire app
             * 
             * Example:
             * ILogger logger = new CloudLogger();
             * Or
             * ILogger logger = new SlackLogger();
             */
            ILogger logger = new ConsoleLogger();
            

            var order = new Order
            {
                Id = 1001,
                Customer = customer,
                OrderLines = new List<OrderLine>()
            };

            var product1 = repo.GetProductById(1);
            var product2 = repo.GetProductById(2);

            order.OrderLines.Add(new OrderLine { Product = product1, Quantity = 2 });
            order.OrderLines.Add(new OrderLine { Product = product2, Quantity = 1 });

            var checkoutService = new CheckoutService(repo,logger);
            checkoutService.ProcessOrder(order);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
