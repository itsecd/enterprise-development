using WarehouseEnterpriceApp;

namespace TESTS;

public class WarehouseEnterpriceFixture
{
    public List<Organization> Organizations =
        [
        new() {Id = 0, Name = "Electronic Arts", Address = "Redwood City, California"},
        new() {Id = 1, Name = "Ubisoft", Address = "Montreuil, France"},
        new() {Id = 2, Name = "Rockstar Games", Address = "New York, USA"},
        new() {Id = 3, Name = "Mojang", Address = "Stockholm, Sweden"},
        new() {Id = 4, Name = "Activision", Address = "Santa Monica, California"},
        new() {Id = 5, Name = "Bethesda Softworks", Address = "Rockville, Maryland"},
        new() {Id = 6, Name = "Blizzard", Address = "Irvine, California"},
        new() {Id = 7, Name = "Epic Games", Address = "Cary, North Carolina."},
        new() {Id = 8, Name = "Naughty Dog", Address = "Santa Monica, California"},
        new() {Id = 9, Name = "Square Enix", Address = "Tokyo, Japan"},
        new() {Id = 10, Name = "CD Projekt Red", Address = "Warsaw, Poland"}
        ];

    public List<Product> Products =
        [
        new() { Id = 0, Code = 1, Name = "The Last of Us Part II" },
        new() { Id = 1, Code = 2, Name = "Cyberpunk 2077" },
        new() { Id = 2, Code = 3, Name = "Ghost of Tsushima" },
        new() { Id = 3, Code = 4, Name = "Assassin's Creed Valhalla" },
        new() { Id = 4, Code = 5, Name = "Call of Duty: Warzone" },
        new() { Id = 5, Code = 6, Name = "Elden Ring" },
        new() { Id = 6, Code = 7, Name = "Final Fantasy VII Remake" },
        new() { Id = 7, Code = 8, Name = "Red Dead Redemption 2" },
        new() { Id = 8, Code = 9, Name = "The Wither III: Wild Hunt" },
        new() { Id = 9, Code = 10, Name = "Overwatch" },
        new() { Id = 10, Code = 11, Name = "Minecraft" }
        ];

    public List<Cell> Cells;

    public List<Supply> Supplies;
    public WarehouseEnterpriceFixture()
    {
        Cells =
        [
        new() { Id = 0, Product = Products[0], Quantity = 8 },  
        new() { Id = 1, Product = Products[1], Quantity = 6 },  
        new() { Id = 2, Product = Products[2], Quantity = 2 },  
        new() { Id = 3, Product = Products[3], Quantity = 15 }, 
        new() { Id = 4, Product = Products[4], Quantity = 3 },  
        new() { Id = 5, Product = Products[5], Quantity = 7 },  
        new() { Id = 6, Product = Products[6], Quantity = 10 }, 
        new() { Id = 7, Product = Products[7], Quantity = 20 }, 
        new() { Id = 8, Product = Products[8], Quantity = 13 }, 
        new() { Id = 9, Product = Products[9], Quantity = 1 },  
        new() { Id = 10, Product = Products[10], Quantity = 20 } 
        ];

        Supplies =
        [
        new() { Id = 0, Product = Products[0], Organization = Organizations[8], Quantity = 10, SupplyDate = new DateTime(2021, 5, 10) }, 
        new() { Id = 1, Product = Products[1], Organization = Organizations[2], Quantity = 20, SupplyDate = new DateTime(2022, 12, 1) }, 
        new() { Id = 2, Product = Products[2], Organization = Organizations[7], Quantity = 15, SupplyDate = new DateTime(2020, 8, 15) }, 
        new() { Id = 3, Product = Products[3], Organization = Organizations[1], Quantity = 5, SupplyDate = new DateTime(2021, 6, 12) }, 
        new() { Id = 4, Product = Products[4], Organization = Organizations[4], Quantity = 30, SupplyDate = new DateTime(2021, 10, 9) }, 
        new() { Id = 5, Product = Products[5], Organization = Organizations[7], Quantity = 25, SupplyDate = new DateTime(2019, 3, 28) }, 
        new() { Id = 6, Product = Products[6], Organization = Organizations[9], Quantity = 12, SupplyDate = new DateTime(2020, 4, 5) }, 
        new() { Id = 7, Product = Products[7], Organization = Organizations[2], Quantity = 8, SupplyDate = new DateTime(2019, 11, 14) }, 
        new() { Id = 8, Product = Products[8], Organization = Organizations[10], Quantity = 18, SupplyDate = new DateTime(2021, 7, 22) }, 
        new() { Id = 9, Product = Products[9], Organization = Organizations[6], Quantity = 14, SupplyDate = new DateTime(2022, 1, 30) }, 
        new() { Id = 10, Product = Products[10], Organization = Organizations[3], Quantity = 50, SupplyDate = new DateTime(2023, 3, 15) } 
        ];
    }
}