namespace EKartBL
{
    // Now responsible only for coordinating the steps
    public class CheckoutService
    {
        private readonly EkartRepository _repository;
        private readonly OrderCalculator _orderCalculator;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly InvoicePrinter _invoicePrinter;

        public CheckoutService(EkartRepository repository)
        {
            _repository = repository;

            // Still creating dependencies directly (DIP violation kept for later)
            _orderCalculator = new OrderCalculator();
            _paymentProcessor = new PaymentProcessor();
            _invoicePrinter = new InvoicePrinter();
        }

        public void ProcessOrder(Order order)
        {
            // 1. Calculate all totals
            _orderCalculator.CalculateTotals(order);

            // 2. Save order
            _repository.SaveOrder(order);

            // 3. Charge payment
            _paymentProcessor.Charge(order);

            // 4. Print invoice
            _invoicePrinter.Print(order);

            // 5. Log completion
            _repository.Log("Order processed successfully: " + order.Id);
        }
    }
}
