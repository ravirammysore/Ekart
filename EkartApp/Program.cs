using System;
using System.Collections.Generic;
using EKartBL;

namespace EKartApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EKart Checkout Demo (Stage 7 - DIP: manual wiring in Program) ===");

            #region Configurations and dependency wirings
            
            // Low-level dependencies
            var repository = new EkartRepository();
            var logger = new ConsoleLogger();

            IEkartRepository repo = repository;
            ILogger log = logger;

            repo.SeedProducts();

            /*Pricing strategies
             * Can be easily swapped here to change behavior
             * say to a different country's tax rules or a different discount policy!
             * Nothing else in the system needs to change!
             */
            ITaxCalculator taxCalculator = new IndiaGstTaxCalculator();
            IDiscountPolicy discountPolicy = new LoyaltyDiscountPolicy();

            IOrderCalculator orderCalculator = new OrderCalculator(taxCalculator, discountPolicy);

            /* Other services
             * Depending on the requirements, we can create abstractions for these too, but keeping it simple for now
             */
            var paymentProcessor = new PaymentProcessor();
            var invoicePrinter = new InvoicePrinter();

            /*High-level policy depends on abstractions, all wired here
             * It also becomes easy to test the system with fakes or mocks for unit testing!
             */
            var checkoutService = new CheckoutService(
                repo,
                log,
                orderCalculator,
                paymentProcessor,
                invoicePrinter);

            #endregion

            #region Place on order for demo

            var customer = new Customer
            {
                Id = 1,
                Name = "Ravi Ram",
                LoyaltyLevel = "Premium"
            };

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

            checkoutService.ProcessOrder(order);

            Console.WriteLine(); 
            #endregion

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
