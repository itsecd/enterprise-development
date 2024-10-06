using WarehouseEnterpriceApp;

namespace TESTS;

public class EnterpriseWarehouseTest(WarehouseEnterpriceFixture fixture) : IClassFixture<WarehouseEnterpriceFixture>
{
    private readonly WarehouseEnterpriceFixture _fixture = fixture;

    [Fact]
    public void ReturnAllProductsSortedByName()
    {
        var expectedData = new List<Product?>
        {
            _fixture.Products[3], 
            _fixture.Products[4], 
            _fixture.Products[1],
            _fixture.Products[5], 
            _fixture.Products[6], 
            _fixture.Products[2], 
            _fixture.Products[10], 
            _fixture.Products[9], 
            _fixture.Products[7], 
            _fixture.Products[0], 
            _fixture.Products[8] 
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
        var name = "Ubisoft";
        var products = _fixture.Supplies
            .Where(s => s.Organization.Name == name && s.SupplyDate == date)
            .Select(s => s.Product)
            .ToList();

        Assert.Equal(new[] { _fixture.Cells[3].Product }, products);
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
            _fixture.Cells[10]
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

        Assert.Equal(new[] { _fixture.Organizations[3] }, result); 
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

        Assert.Equal("Red Dead Redemption 2", products[0].ProductName); 
        Assert.Equal(20, products[0].Quantity);
        Assert.Equal("Minecraft", products[1].ProductName);
        Assert.Equal(20, products[1].Quantity);
        Assert.Equal("Assassin's Creed Valhalla", products[2].ProductName);
        Assert.Equal(15, products[2].Quantity);
        Assert.Equal("The Wither III: Wild Hunt", products[3].ProductName);
        Assert.Equal(13, products[3].Quantity);
        Assert.Equal("Final Fantasy VII Remake", products[4].ProductName);
        Assert.Equal(10, products[4].Quantity);
    }

    [Fact]
    public void ReturnQuantityProductSupplyToOrganizations()
    {
        var expectedData = new[]
        {
            new { TotalQuantity = 50, ProductName = "Minecraft", OrganizationName = "Mojang" },
            new { TotalQuantity = 30, ProductName = "Call of Duty: Warzone", OrganizationName = "Activision" },
            new { TotalQuantity = 25, ProductName = "Elden Ring", OrganizationName = "Epic Games" },
            new { TotalQuantity = 20, ProductName = "Cyberpunk 2077", OrganizationName = "Rockstar Games" },
            new { TotalQuantity = 18, ProductName = "The Wither III: Wild Hunt", OrganizationName = "CD Projekt Red" },
            new { TotalQuantity = 15, ProductName = "Ghost of Tsushima", OrganizationName = "Epic Games" },
            new { TotalQuantity = 14, ProductName = "Overwatch", OrganizationName = "Blizzard" },
            new { TotalQuantity = 12, ProductName = "Final Fantasy VII Remake", OrganizationName = "Square Enix" },
            new { TotalQuantity = 10, ProductName = "The Last of Us Part II", OrganizationName = "Naughty Dog" },
            new { TotalQuantity = 8, ProductName = "Red Dead Redemption 2", OrganizationName = "Rockstar Games" },
            new { TotalQuantity = 5, ProductName = "Assassin's Creed Valhalla", OrganizationName = "Ubisoft" }
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
}
