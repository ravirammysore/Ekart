using System;
using System.Collections.Generic;

namespace EKartBL
{
    // Now: only data access related implementations inside this class!
    public class EkartRepository: IEkartRepository
    {
        // In-memory "database"
        private readonly List<Product> _products = new List<Product>();
        private readonly List<Order> _orders = new List<Order>();

        // Seeds data and acts as a fake DB initializer
        public void SeedProducts()
        {
            _products.Clear();
            _products.Add(new Product { Id = 1, Name = "Laptop Bag", UnitPrice = 1500m });
            _products.Add(new Product { Id = 2, Name = "Wireless Mouse", UnitPrice = 800m });
            _products.Add(new Product { Id = 3, Name = "USB-C Cable", UnitPrice = 300m });
        }

        public Product GetProductById(int id)
        {
            foreach (var p in _products)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }

            // Very bad fallback – intentionally simple / wrong
            return new Product { Id = id, Name = "Unknown Product", UnitPrice = 0m };
        }

        public void SaveOrder(Order order)
        {
            _orders.Add(order);
            Console.WriteLine();
            Console.WriteLine("Order saved in in-memory repository. Current order count: " + _orders.Count);
        }
        
        //**this is also a candidate for removal from this class, but keeping the demo simple for now!**
        public void ExportOrdersToCsv(string filePath)
        {
            // Not implemented – just a placeholder
            Console.WriteLine("(Export to CSV not implemented, but this method lives here anyway.)");
        }
    }
}
