using System;

namespace EKartBL
{
    // One big service that does "everything" (violates SRP, OCP, DIP, etc.)
    public class CheckoutService
    {
        private readonly EkartRepository _repository;

        public CheckoutService(EkartRepository repository)
        {
            _repository = repository;
        }

        public void ProcessOrder(Order order)
        {
            // 1. Calculate subtotal
            decimal subtotal = 0m;
            foreach (var line in order.OrderLines)
            {
                subtotal += line.Product.UnitPrice * line.Quantity;
            }
            order.SubTotal = subtotal;

            // 2. Apply discount based on loyalty level
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
                // If we ever add any new loyalty level, we must modify this code. (OCP violation)
                discountPercentage = 0m;
            }

            order.DiscountAmount = (subtotal * discountPercentage) / 100m;
            decimal amountAfterDiscount = subtotal - order.DiscountAmount;

            // 3. Calculate tax (hard-coded, India-only 18%)
            decimal taxPercentage = 18m;
            order.TaxAmount = (amountAfterDiscount * taxPercentage) / 100m;

            // 4. Grand total
            order.GrandTotal = amountAfterDiscount + order.TaxAmount;

            // 5. "Save" the order somewhere
            _repository.SaveOrder(order);

            // 6. Charge payment (just a console message)
            ChargePayment(order);

            // 7. Print invoice to console
            PrintInvoice(order);

            // 8. Log that we finished processing
            _repository.Log("Order processed successfully: " + order.Id);
        }

        private void ChargePayment(Order order)
        {
            Console.WriteLine();
            Console.WriteLine("Charging payment of INR " + order.GrandTotal + " for customer " + order.Customer.Name + "...");
            Console.WriteLine("(Pretend payment was successful)");
        }

        private void PrintInvoice(Order order)
        {
            Console.WriteLine();
            Console.WriteLine("========= EKart Invoice =========");
            Console.WriteLine("Order Id   : " + order.Id);
            Console.WriteLine("Customer   : " + order.Customer.Name + " (" + order.Customer.LoyaltyLevel + ")");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Items:");
            foreach (var line in order.OrderLines)
            {
                decimal lineTotal = line.Product.UnitPrice * line.Quantity;
                Console.WriteLine($" - {line.Product.Name} x {line.Quantity} @ {line.Product.UnitPrice} = {lineTotal}");
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
