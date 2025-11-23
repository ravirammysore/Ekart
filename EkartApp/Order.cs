using System.Collections.Generic;

namespace EKartApp
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
