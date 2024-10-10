using StoreCashFlow.Domain;

namespace StoreCashFlow.Api.Service;

public class ProductService
{
    private List<Product> _products = [];
    public Product AddProduct(Product newProduct)
    {
        _products.Add(newProduct);
        return newProduct;
    }

    public List<Product> GetProducts()
    {
        return _products;
    }
    public Product? GetProductById(string id)
    {
        return _products.First(c => c.Barcode == id);
    }

    public bool DeleteProduct(string id)
    {
        var product = GetProductById(id);
        if (product == null)
        {
            return false;
        }
        _products.Remove(product);
        return true;
    }

    public bool UpdateProduct(Product updateProduct)
    {
        var product = GetProductById(updateProduct.Barcode);
        if (product == null)
        {
            return false;
        }
        product.Name = updateProduct.Name;
        product.Price = updateProduct.Price;
        product.ProductGroupCode = updateProduct.ProductGroupCode;
        product.ProductType = updateProduct.ProductType;
        product.Weight = updateProduct.Weight;
        product.ExpirationDate = updateProduct.ExpirationDate;
        return true;
    }
}