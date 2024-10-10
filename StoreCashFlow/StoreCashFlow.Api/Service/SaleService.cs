using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Service;

public class SaleService
{
    private List<Sale> _sales = [];
    private int _saleId = 1;
    public Sale AddSale(Sale newSale)
    {
        newSale.SaleId = _saleId++;
        _sales.Add(newSale);
        return newSale;
    }

    public List<Sale> GetSales()
    {
        return _sales;
    }
    public Sale? GetSaleById(int id)
    {
        return _sales.First(c => c.SaleId == id);
    }

    public bool DeleteSale(int id)
    {
        var sale = GetSaleById(id);
        if (sale == null)
        {
            return false;
        }
        _sales.Remove(sale);
        return true;
    }

    public bool UpdateSale(Sale updateSale)
    {
        var sale = GetSaleById(updateSale.SaleId);
        if (sale == null)
        {
            return false;
        }
        sale.SaleDate = updateSale.SaleDate;
        sale.Product = updateSale.Product;
        sale.Quantity = updateSale.Quantity;
        sale.Store = updateSale.Store;
        sale.Customer = updateSale.Customer;
        return true;
    }
}
