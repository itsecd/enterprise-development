using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.API.Services;

public class ProductService
{
    private readonly List<Product> _products = [];

    private int _id = 1;

    public List<Product> GetProducts()
    {
        return _products;
    }

    public Product? GetProductById(int id)
    {
        return _products.Find(o => o.Id == id);
    }

    public Product AddProduct(Product product)
    {
        product.Id = _id++;
        _products.Add(product);
        return product;
    }

    public bool DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product == null)
        {
            return false;
        }
        _products.Remove(product);
        return true;
    }

    public bool UpdateProduct(Product updatedProduct)
    {
        var product = GetProductById(updatedProduct.Id);
        if (product == null)
        {
            return false;
        }
        product.Name = updatedProduct.Name;
        product.Code = updatedProduct.Code;
        return true;
    }
}
