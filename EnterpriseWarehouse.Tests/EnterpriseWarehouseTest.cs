using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.Tests;

public class EnterpriseWarehouseTest(WarehouseFixture fixture) : IClassFixture<WarehouseFixture>
{
    private readonly WarehouseFixture _fixture = fixture;

    [Fact]
    public void ReturnAllProductsSortedByName()
    {
        var expectedData = new List<Product?>
        {
            _fixture.Products[9],
            _fixture.Products[3],
            _fixture.Products[0],
            _fixture.Products[5],
            _fixture.Products[2],
            _fixture.Products[10],
            _fixture.Products[4],
            _fixture.Products[6],
            _fixture.Products[8],
            _fixture.Products[7],
            _fixture.Products[1],
            _fixture.Products[11]
        }; 
        var sortedProducts = _fixture.Cells
            .OrderBy(c => c.Product?.Name)
            .Select(c => c.Product)
            .ToList();
        Assert.Equal(expectedData, sortedProducts);
    }

    [Fact]
    public void ReturnProductsRecieveOnDate()
    {
        var date = new DateTime(2021, 6, 12);
        var name = "Beta Distributors";
        var products = _fixture.Supplies
            .Where(s => s.Organization.Name == name && s.SupplyDate == date)
            .Select(s => s.Product)
            .ToList();
        Assert.Equal([ _fixture.Cells[3].Product ], products);
    }

    [Fact]
    public void ReturnCurrentWarehouseState()
    {
        var expectedData = new List<Cell>
        {
            _fixture.Cells[0],
            _fixture.Cells[1],
            _fixture.Cells[2],
            _fixture.Cells[3],
            _fixture.Cells[4],
            _fixture.Cells[5],
            _fixture.Cells[6],
            _fixture.Cells[7],
            _fixture.Cells[8],
            _fixture.Cells[9],
            _fixture.Cells[10],
            _fixture.Cells[11]
        };

        Assert.Equal(expectedData, _fixture.Cells);
    }

    [Fact]
    public void ReturnMaxSuppliesOrganizations()
    {
        var startDate = new DateTime(2010, 1, 1);
        var endDate = new DateTime(2023, 12, 31);
        var organizationsWithMaxSupply = _fixture.Supplies
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

        Assert.Equal([_fixture.Organizations[6]], result);
    }

    [Fact]
    public void ReturnFiveMaxQuantityProducts()
    {
        var products = _fixture.Cells
            .GroupBy(c => c.Product?.Name)
            .Select(g => new
            {
                ProductName = g.Key,
                Quantity = g.Sum(c => c.Quantity),
            })
            .OrderByDescending(p => p.Quantity)
            .Take(5)
            .ToList();

        Assert.Equal("Monitor", products[0].ProductName);
        Assert.Equal(22, products[0].Quantity);
        Assert.Equal("Smartphone", products[1].ProductName);
        Assert.Equal(20, products[1].Quantity);
        Assert.Equal("Webcam", products[2].ProductName);
        Assert.Equal(16, products[2].Quantity);
        Assert.Equal("Laptop", products[3].ProductName);
        Assert.Equal(15, products[3].Quantity);
        Assert.Equal("Keyboard", products[4].ProductName);
        Assert.Equal(15, products[4].Quantity);
    }

    [Fact]
    public void ReturnQuantityProductSupplyToOrganiztions()
    {
        var expectedData = new[]
        {
            new{ TotalQuantity = 159, ProductName = "Printer", OrganizationName = "Techno Group" },
            new{ TotalQuantity = 33, ProductName = "Laptop", OrganizationName = "Enterprise Inc." },
            new{ TotalQuantity = 22, ProductName = "Mouse", OrganizationName = "Smart Systems" },
            new{ TotalQuantity = 19, ProductName = "Mouse", OrganizationName = "Global Trade Ltd" },
            new{ TotalQuantity = 16, ProductName = "Tablet", OrganizationName = "Innovate Solutions" },
            new{ TotalQuantity = 8, ProductName = "Monitor", OrganizationName = "Alpha Logistics" },
            new{ TotalQuantity = 5, ProductName = "Keyboard", OrganizationName = "Beta Distributors" },
            new{ TotalQuantity = 3, ProductName = "Laptop", OrganizationName = "TechCorp" },
            new{ TotalQuantity = 1, ProductName = "Smartphone", OrganizationName = "Smart Systems" }

        };

        var info = _fixture.Supplies
            .GroupBy(p => new { ProductName = p.Product.Name, OrganizationName = p.Organization.Name })
            .Select(g => new
            {
                TotalQuantity = g.Sum(p => p.Quantity),
                g.Key.ProductName,
                g.Key.OrganizationName,
            })
            .OrderByDescending(p => p.TotalQuantity)
            .ToList();

        Assert.Equal(expectedData, info);
    }
};