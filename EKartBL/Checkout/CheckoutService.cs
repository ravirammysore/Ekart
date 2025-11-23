using EKartBL;

namespace EKartBL
{
    // Coordinates steps; still "new"s concrete implementations (DIP later)

    /*Notice that we are now using IEkartRepository instead of EkartRepository directly! This gives us flexibility to swap different repository implementations in the future!
     * But does CheckoutService need to know about all the methods in IEkartRepository? (ISP discussion later)
     */
    public class CheckoutService
    {
        private readonly IEkartRepository _repository;
        private readonly OrderCalculator _orderCalculator;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly InvoicePrinter _invoicePrinter;

        public CheckoutService(IEkartRepository repository)
        {
            _repository = repository;

            // OCP: we can swap these strategies without changing OrderCalculator
            ITaxCalculator taxCalculator = new IndiaGstTaxCalculator();
            IDiscountPolicy discountPolicy = new LoyaltyDiscountPolicy();

            _orderCalculator = new OrderCalculator(taxCalculator, discountPolicy);
            _paymentProcessor = new PaymentProcessor();
            _invoicePrinter = new InvoicePrinter();
        }

        public void ProcessOrder(Order order)
        {
            // 1. Calculate all totals
            _orderCalculator.CalculateTotals(order);

            // 2. Save
            _repository.SaveOrder(order);

            // 3. Charge payment
            _paymentProcessor.Charge(order);

            // 4. Print invoice
            _invoicePrinter.Print(order);

            // 5. Log
            _repository.Log("Order processed successfully: " + order.Id);
        }
    }
}
