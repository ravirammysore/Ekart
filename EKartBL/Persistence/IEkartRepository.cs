namespace EKartBL
{
    // Now focused only on data access for EKart
    //We removed Log and ExportOrdersToCsv from the interface!
    public interface IEkartRepository
    {
        void SeedProducts();
        Product GetProductById(int id);   
        void SaveOrder(Order order);
    }
}
