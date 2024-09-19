namespace DistrictEnterpriseStatisticalData.Domain.Test;

public class DataProvider
{
    public List<Supply> Supplies =
    [
        new(){ SupplyId = 1, Quantity = 1, Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 2, Quantity = 2, Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 3, Quantity = 3, Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 4, Quantity = 4, Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 5, Quantity = 5, Date = DateOnly.ParseExact("2021-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 6, Quantity = 6, Date = DateOnly.ParseExact("2021-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 7, Quantity = 7, Date = DateOnly.ParseExact("2021-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 8, Quantity = 8, Date = DateOnly.ParseExact("2022-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 9, Quantity = 9, Date = DateOnly.ParseExact("2022-01-01", "yyyy-mm-dd") },
        new(){ SupplyId = 10, Quantity = 10, Date = DateOnly.ParseExact("2022-01-01", "yyyy-mm-dd") },
    ];

    public List<Enterprise> Enterprises =
    [
        new() {
            RegistrationNumber = 1,
            Type = EnterpriseType.Agriculture,
            Name = "Enterprise 1",
            Address = "Address 1",
            PhoneNumber = "PhoneNumber 1",
            Form = FormOfOwnership.Private,
            EmployeesNumber = 1,
            TotalArea = 1
        },
        new() {
            RegistrationNumber = 2,
            Type = EnterpriseType.Agriculture,
            Name = "Enterprise 2",
            Address = "Address 2",
            PhoneNumber = "PhoneNumber 2",
            Form = FormOfOwnership.JointStock,
            EmployeesNumber = 2,
            TotalArea = 2
        },
        new() {
            RegistrationNumber = 3,
            Type = EnterpriseType.HeavyIndustry,
            Name = "Enterprise 3",
            Address = "Address 3",
            PhoneNumber = "PhoneNumber 3",
            Form = FormOfOwnership.MunicipalUrban,
            EmployeesNumber = 3,
            TotalArea = 3
        },
        new() {
            RegistrationNumber = 4,
            Type = EnterpriseType.HeavyIndustry,
            Name = "Enterprise 4",
            Address = "Address 4",
            PhoneNumber = "PhoneNumber 4",
            Form = FormOfOwnership.StateFederal,
            EmployeesNumber = 4,
            TotalArea = 4
        },
        new() {
            RegistrationNumber = 5,
            Type = EnterpriseType.MaterialAndTechnicalSupply,
            Name = "Enterprise 5",
            Address = "Address 5",
            PhoneNumber = "PhoneNumber 5",
            Form = FormOfOwnership.TOO,
            EmployeesNumber = 5,
            TotalArea = 5
        },
        new() {
            RegistrationNumber = 6,
            Type = EnterpriseType.MaterialAndTechnicalSupply,
            Name = "Enterprise 6",
            Address = "Address 6",
            PhoneNumber = "PhoneNumber 6",
            Form = FormOfOwnership.Private,
            EmployeesNumber = 6,
            TotalArea = 6
        },
        new() {
            RegistrationNumber = 7,
            Type = EnterpriseType.LightIndustry,
            Name = "Enterprise 7",
            Address = "Address 7",
            PhoneNumber = "PhoneNumber 7",
            Form = FormOfOwnership.TOO,
            EmployeesNumber = 7,
            TotalArea = 7
        },
        new() {
            RegistrationNumber = 8,
            Type = EnterpriseType.LightIndustry,
            Name = "Enterprise 8",
            Address = "Address 8",
            PhoneNumber = "PhoneNumber 8",
            Form = FormOfOwnership.MunicipalUrban,
            EmployeesNumber = 8,
            TotalArea = 8
        },
        new() {
            RegistrationNumber = 9,
            Type = EnterpriseType.Building,
            Name = "Enterprise 9",
            Address = "Address 9",
            PhoneNumber = "PhoneNumber 9",
            Form = FormOfOwnership.MunicipalUrban,
            EmployeesNumber = 9,
            TotalArea = 9
        },
        new() {
            RegistrationNumber = 10,
            Type = EnterpriseType.Transport,
            Name = "Enterprise 10",
            Address = "Address 10",
            PhoneNumber = "PhoneNumber 10",
            Form = FormOfOwnership.TOO,
            EmployeesNumber = 10,
            TotalArea = 10
        },
    ];

    public List<Supplier> Suppliers;

    public DataProvider()
    {
        Suppliers =
        [
            new Supplier { 
                Id = 1, 
                Name = "Supplier 1",
                Supplies = Supplies.Slice(1, 2), 
                Enterprises = [Enterprises[0], Enterprises[1]]
            },
            new Supplier { 
                Id = 2, 
                Name = "Supplier 2",
                Supplies = Supplies.Slice(3, 1), 
                Enterprises = [Enterprises[5], Enterprises[6], Enterprises[1]]
            },
            new Supplier { 
                Id = 3, 
                Name = "Supplier 3",
                Supplies = Supplies.Slice(6, 3), 
                Enterprises = [Enterprises[1], Enterprises[7], Enterprises[1]]
            },
            new Supplier { 
                Id = 4, 
                Name = "Supplier 4",
                Supplies = Supplies.Slice(1, 5), 
                Enterprises = [Enterprises[8], Enterprises[1]]
            },
            new Supplier { 
                Id = 5, 
                Name = "Supplier 5",
                Supplies = Supplies.Slice(8, 1), 
                Enterprises = [Enterprises[0], Enterprises[9], Enterprises[1]]
            },
        ];
        Enterprises[0].Suppliers = [Suppliers[0], Suppliers[4]];
        Enterprises[1].Suppliers = [Suppliers[0], Suppliers[1], Suppliers[2], Suppliers[3], Suppliers[4]];
        Enterprises[5].Suppliers = [Suppliers[1]];
        Enterprises[6].Suppliers = [Suppliers[1]];
        Enterprises[7].Suppliers = [Suppliers[2]];
        Enterprises[8].Suppliers = [Suppliers[3]];
        Enterprises[9].Suppliers = [Suppliers[4]];
    }
}