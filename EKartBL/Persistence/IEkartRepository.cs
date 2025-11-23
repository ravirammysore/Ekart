namespace EKartBL
{   
    public interface IEkartRepository
    {
        void SeedProducts();
        Product GetProductById(int id);        
        void SaveOrder(Order order);  
        void Log(string message);
        void ExportOrdersToCsv(string filePath);
    }

    /*
     * Notice that this interface is "fat" and seems to be doing too many things. 
     * This will force classes that implement this interface to provide implementations for methods they might not need.
     * This is intentional to illustrate the Interface Segregation Principle (ISP) later.
     */
}
