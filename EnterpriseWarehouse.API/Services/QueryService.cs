using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.API.Services;

public class QueryService(CellService cellService, SupplyService supplyService)
{
    public List<Product?> GetAllProductsSortedByName()
    {
        var sortedProducts = cellService.GetCells()
            .OrderBy(c => c.Product?.Name)
            .Select(c => c.Product)
            .ToList();
        return sortedProducts;
    }

    public List<Product> GetProductsRecieveOnDate(string name, DateTime date)
    {
        var products = supplyService.GetSupplies()
            .Where(s => s.Organization.Name == name && s.SupplyDate == date)
            .Select(s => s.Product)
            .ToList();
        return products;
    }

    public List<Cell> GetCurrentWarehouseState()
    {
        return cellService.GetCells();
    }

    public List<Organization> GetMaxSuppliesOrganizations(DateTime startDate, DateTime endDate)
    {
        var organizationsWithMaxSupply = supplyService.GetSupplies()
            .Where(s => s.SupplyDate >= startDate && s.SupplyDate <= endDate)
            .GroupBy(s => s.Organization)
            .Select(g => new
            {
                Organization = g.Key,
                TotalQuantity = g.Sum(s => s.Quantity)
            })
            .ToList();
        var maxQuantity = organizationsWithMaxSupply.Max(o => o.TotalQuantity);
        var result = organizationsWithMaxSupply
            .Where(o => o.TotalQuantity == maxQuantity)
            .Select(o => o.Organization)
            .ToList();

        return result;
    }

    public List<ProductQuantityDTO> GetFiveMaxQuantityProducts()
    {
        var products = cellService.GetCells()
            .GroupBy(c => c.Product?.Name)
            .Select(g => new ProductQuantityDTO
            {
                ProductName = g.Key,
                Quantity = g.Sum(c => c.Quantity),
            })
            .OrderByDescending(p => p.Quantity)
            .Take(5)
            .ToList();
        return products;
    }

    public List<ProductSupplyToOrganizationsDTO> GetQuantityProductSupplyToOrganiztions()
    {
        var info = supplyService.GetSupplies()
            .GroupBy(p => new { ProductName = p.Product.Name, OrganizationName = p.Organization.Name })
            .Select(g => new ProductSupplyToOrganizationsDTO
            {
                TotalQuantity = g.Sum(p => p.Quantity),
                ProductName = g.Key.ProductName,
                OrganizationName = g.Key.OrganizationName,
            })
            .OrderByDescending(p => p.TotalQuantity)
            .ToList();
        return info;
    }
}
