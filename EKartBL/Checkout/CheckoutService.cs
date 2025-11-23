namespace EKartBL
{
    //No more direct dependencies on concrete implementations, i.e IndiaGstTaxCalculator or LoyaltyDiscountPolicy since we are not instantiating them here with the 'new' keyword!
    public class CheckoutService
    {
        private readonly IEkartRepository _repository;
        private readonly ILogger _logger;
        private readonly IOrderCalculator _orderCalculator;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly InvoicePrinter _invoicePrinter;

        public CheckoutService(
            IEkartRepository repository,
            ILogger logger,
            IOrderCalculator orderCalculator,
            PaymentProcessor paymentProcessor,
            InvoicePrinter invoicePrinter)
        {
            _repository = repository;
            _logger = logger;
            _orderCalculator = orderCalculator;
            _paymentProcessor = paymentProcessor;
            _invoicePrinter = invoicePrinter;

            //Notice: no new IndiaGstTaxCalculator() or new LoyaltyDiscountPolicy() here anymore!
        }

        public void ProcessOrder(Order order)
        {
            _orderCalculator.CalculateTotals(order);

            _repository.SaveOrder(order);

            _paymentProcessor.Charge(order);

            _invoicePrinter.Print(order);

            _logger.Log("Order processed successfully: " + order.Id);
        }
    }
}
