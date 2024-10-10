using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Service;

public class ProductTypeService
{
    private List<ProductType> _productTypes = [];
    private int _productTypeId = 1;
    public ProductType AddProductType(ProductType newProductType)
    {
        newProductType.Id = _productTypeId++;
        _productTypes.Add(newProductType);
        return newProductType;
    }

    public List<ProductType> GetProductTypes()
    {
        return _productTypes;
    }
    public ProductType? GetProductTypeById(int id)
    {
        return _productTypes.First(c => c.Id == id);
    }

    public bool DeleteProductType(int id)
    {
        var productType = GetProductTypeById(id);
        if (productType == null)
        {
            return false;
        }
        _productTypes.Remove(productType);
        return true;
    }

    public bool UpdateProductType(ProductType updateProductType)
    {
        var productType = GetProductTypeById(updateProductType.Id);
        if (productType == null)
        {
            return false;
        }
        productType.Name = updateProductType.Name;
        return true;
    }
}
