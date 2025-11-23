namespace EKartBL
{
    // Coordinates steps; still "new"s concrete implementations (DIP later)
    public class CheckoutService
    {
        private readonly EkartRepository _repository;
        private readonly OrderCalculator _orderCalculator;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly InvoicePrinter _invoicePrinter;

        public CheckoutService(EkartRepository repository)
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
