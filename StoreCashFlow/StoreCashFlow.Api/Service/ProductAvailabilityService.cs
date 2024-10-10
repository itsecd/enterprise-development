using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Service;

public class ProductAvailabilityService
{
    private List<ProductAvailability> _productAvailabilities = [];
    private int _productAvailabilityId = 1;
    public ProductAvailability AddProductAvailability(ProductAvailability newProductAvailability)
    {
        newProductAvailability.Id = _productAvailabilityId++;
        _productAvailabilities.Add(newProductAvailability);
        return newProductAvailability;
    }

    public List<ProductAvailability> GetProductAvailabilities()
    {
        return _productAvailabilities;
    }
    public ProductAvailability? GetProductAvailabilityById(int id)
    {
        return _productAvailabilities.First(c => c.Id == id);
    }

    public bool DeleteProductAvailability(int id)
    {
        var productAvailability = GetProductAvailabilityById(id);
        if (productAvailability == null)
        {
            return false;
        }
        _productAvailabilities.Remove(productAvailability);
        return true;
    }

    public bool UpdateProductAvailability(ProductAvailability updateProductAvailability)
    {
        var productAvailability = GetProductAvailabilityById(updateProductAvailability.Id);
        if (productAvailability == null)
        {
            return false;
        }
        productAvailability.Store = updateProductAvailability.Store;
        productAvailability.Product = updateProductAvailability.Product;
        productAvailability.Quantity = updateProductAvailability.Quantity;
        return true;
    }
}
