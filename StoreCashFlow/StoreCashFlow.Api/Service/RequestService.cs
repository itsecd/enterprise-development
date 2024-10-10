using StoreCashFlow.Api.DTO;
using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Service;

public class RequestService(ProductAvailabilityService productAvailabilityService, SaleService saleService)
{
    public List<Product> ReturnAllProductsInStore(int id)
    {
        return (
            productAvailabilityService.GetProductAvailabilities()
            .Where(pa => pa.Store.StoreId == id)
            .Select(pa => pa.Product)
            .ToList()
        );
    }
    public List<Store> ReturnStoresWithProductInStock(string barcode)
    {
        return (
            productAvailabilityService.GetProductAvailabilities()
            .Where(pa => pa.Product.Barcode == barcode)
            .Select(pa => pa.Store)
            .ToList()
        );
    }
    public List<ProductPriceInfoDto> ReturnAveragePriceByGroupAndStore()
    {
        return (
            productAvailabilityService.GetProductAvailabilities()
            .GroupBy(pa => new { pa.Store.StoreId, pa.Product.ProductGroupCode })
            .Select(group => new ProductPriceInfoDto
            {
                StoreId = group.Key.StoreId,
                ProductGroupCode = group.Key.ProductGroupCode,
                AvgPrice = group.Average(pa => pa.Product.Price)
            })
            .ToList()
        );
    }
    public List<SaleInfoDto> ReturnTop5SalesByTotalAmount()
    {
        return (
            saleService.GetSales()
            .GroupBy(s => s.Product.ProductGroupCode)
            .Select(group => new SaleInfoDto
            {
                ProductName = group.First().Product.Name,
                TotalAmount = Math.Round(group.Sum(s => s.Product.Price * s.Quantity), 2)
            })
            .OrderByDescending(g => g.TotalAmount)
            .Take(5)
            .ToList()
        );
    }
    public List<ExpiredProductInfoDto> ReturnExpiredProducts(DateTime expirationDate)
    {
        return (
            productAvailabilityService.GetProductAvailabilities()
            .Where(pa => pa.Product.ExpirationDate < expirationDate)
            .Select(pa => new ExpiredProductInfoDto
            {
                Product = pa.Product,
                Store = pa.Store
            })
            .ToList()
        );
    }
    public List<HighSalesDto> GetStoresWithHighSales(DateTime monthStart, DateTime monthEnd, double threshold)
    {
        return (
            saleService.GetSales()
            .Where(s => s.SaleDate >= monthStart && s.SaleDate <= monthEnd)
            .GroupBy(s => s.Store.StoreId)
            .Select(group => new HighSalesDto
            {
                StoreId = group.Key,
                TotalSales = Math.Round(group.Sum(s => s.Quantity * s.Product.Price), 2)
            })
            .Where(result => result.TotalSales > threshold)
            .ToList()
        );
    }
}
