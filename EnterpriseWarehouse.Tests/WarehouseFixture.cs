using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.Tests;

public class WarehouseFixture
{
    public List<Organization> Organizations =
    [
        new() {Id = 0, Name = "TechCorp", Address = "123 Main St, Springfield"},
        new() {Id = 1, Name = "Innovate Solutions", Address = "456 Elm St, Metropolis"},
        new() {Id = 2, Name = "Alpha Logistics", Address = "789 Oak St, Gotham"},
        new() {Id = 3, Name = "Beta Distributors", Address = "101 Maple Ave, Star City"},
        new() {Id = 4, Name = "Global Trade Ltd", Address = "202 Birch St, Central City"},
        new() {Id = 5, Name = "Enterprise Inc.", Address = "303 Cedar St, Keystone City"},
        new() {Id = 6, Name = "Techno Group", Address = "404 Pine St, Coast City"},
        new() {Id = 7, Name = "Smart Systems", Address = "505 Willow Ave, Blüdhaven"},
        new() {Id = 8, Name = "Quantum Enterprises", Address = "606 Redwood St, Hub City"},
        new() {Id = 9, Name = "GreenEnergy Corp", Address = "707 Cypress St, Fawcett City"}
    ];

    public List<Product> Products = 
    [
        new() { Id = 0, Code = 1, Name = "Laptop" },
        new() { Id = 1, Code = 2, Name = "Tablet" },
        new() { Id = 2, Code = 3, Name = "Monitor" },
        new() { Id = 3, Code = 4, Name = "Keyboard" },
        new() { Id = 4, Code = 5, Name = "Mouse" },
        new() { Id = 5, Code = 1, Name = "Laptop" },
        new() { Id = 6, Code = 6, Name = "Printer" },
        new() { Id = 7, Code = 7, Name = "Smartphone" },
        new() { Id = 8, Code = 8, Name = "Router" },
        new() { Id = 9, Code = 9, Name = "Headphones" },
        new() { Id = 10, Code = 3, Name = "Monitor" },
        new() { Id = 11, Code = 10, Name = "Webcam" }
    ];

    public List<Cell> Cells;

    public List<Supply> Supplies;
    public WarehouseFixture()
    {
        Cells =
        [
            new() {Id = 0, Product = Products[0], Quantity = 8},
            new() {Id = 1, Product = Products[1], Quantity = 6},
            new() {Id = 2, Product = Products[2], Quantity = 2},
            new() {Id = 3, Product = Products[3], Quantity = 15},
            new() {Id = 4, Product = Products[4], Quantity = 3},
            new() {Id = 5, Product = Products[5], Quantity = 7},
            new() {Id = 6, Product = Products[6], Quantity = 10},
            new() {Id = 7, Product = Products[7], Quantity = 20},
            new() {Id = 8, Product = Products[8], Quantity = 13},
            new() {Id = 9, Product = Products[9], Quantity = 1},
            new() {Id = 10, Product = Products[10], Quantity = 20},
            new() {Id = 11, Product = Products[11], Quantity = 16}
        ];
        Supplies =
        [
            new() {Id = 0, Product = Products[0], Organization = Organizations[0], Quantity = 3, SupplyDate = new DateTime(2015, 2, 3) },
            new() {Id = 1, Product = Products[1], Organization = Organizations[1], Quantity = 16, SupplyDate = new DateTime(2022, 11, 15) },
            new() {Id = 2, Product = Products[2], Organization = Organizations[2], Quantity = 8, SupplyDate = new DateTime(2019, 7, 22) },
            new() {Id = 3, Product = Products[3], Organization = Organizations[3], Quantity = 5, SupplyDate = new DateTime(2021, 6, 12) },
            new() {Id = 4, Product = Products[4], Organization = Organizations[4], Quantity = 19, SupplyDate = new DateTime(2021, 10, 9) },
            new() {Id = 5, Product = Products[5], Organization = Organizations[5], Quantity = 33, SupplyDate = new DateTime(2013, 1, 28) },
            new() {Id = 6, Product = Products[6], Organization = Organizations[6], Quantity = 159, SupplyDate = new DateTime(2015, 4, 5) },
            new() {Id = 7, Product = Products[7], Organization = Organizations[7], Quantity = 1, SupplyDate = new DateTime(2011, 8, 14) },
            new() {Id = 8, Product = Products[4], Organization = Organizations[7], Quantity = 6, SupplyDate = new DateTime(2016, 4, 12) },
            new() {Id = 9, Product = Products[4], Organization = Organizations[7], Quantity = 16, SupplyDate = new DateTime(2013, 2, 15) }
        ];
    }
}
