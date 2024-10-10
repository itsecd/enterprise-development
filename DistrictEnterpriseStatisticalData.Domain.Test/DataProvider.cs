using DistrictEnterpriseStatisticalData.Domain.Entity;

namespace DistrictEnterpriseStatisticalData.Domain.Test;

public class DataProvider
{
    public List<EnterpriseType> EnterpriseTypes =
    [
        new() { Type = "Agriculture" },
        new() { Type = "Transport" },
        new() { Type = "LightIndustry" },
        new() { Type = "HeavyIndustry" },
        new() { Type = "Building" },
        new() { Type = "MaterialAndTechnicalSupply" },
    ];

    public List<FormOfOwnership> FormsOfOwnership =
    [
        new() { Form = "StateFederal" },
        new() { Form = "MunicipalUrban" },
        new() { Form = "TOO" },
        new() { Form = "Private" },
        new() { Form = "JointStock" },
    ];

    public List<Supply> Supplies =
    [
        new()
        {
            EnterpriseRegistrationNumber = 0, SupplierId = 0, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 0, SupplierId = 4, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 1, SupplierId = 0, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 1, SupplierId = 1, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 1, SupplierId = 2, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 1, SupplierId = 3, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 1, SupplierId = 4, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 5, SupplierId = 1, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 6, SupplierId = 1, Quantity = 1,
            Date = DateOnly.ParseExact("2020-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 7, SupplierId = 2, Quantity = 1,
            Date = DateOnly.ParseExact("2021-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 8, SupplierId = 3, Quantity = 1,
            Date = DateOnly.ParseExact("2021-01-01", "yyyy-mm-dd")
        },
        new()
        {
            EnterpriseRegistrationNumber = 9, SupplierId = 4, Quantity = 1,
            Date = DateOnly.ParseExact("2022-01-01", "yyyy-mm-dd")
        },
    ];

    public List<Enterprise> Enterprises =
    [
        new()
        {
            EnterpriseTypeId = 0,
            Name = "Enterprise 1",
            Address = "Address 1",
            PhoneNumber = "PhoneNum 1",
            FormId = 3,
            EmployeesNumber = 1,
            TotalArea = 1
        },
        new()
        {
            EnterpriseTypeId = 0,
            Name = "Enterprise 2",
            Address = "Address 2",
            PhoneNumber = "PhoneNum 2",
            FormId = 4,
            EmployeesNumber = 2,
            TotalArea = 2
        },
        new()
        {
            EnterpriseTypeId = 3,
            Name = "Enterprise 3",
            Address = "Address 3",
            PhoneNumber = "PhoneNum 3",
            FormId = 1,
            EmployeesNumber = 3,
            TotalArea = 3
        },
        new()
        {
            EnterpriseTypeId = 3,
            Name = "Enterprise 4",
            Address = "Address 4",
            PhoneNumber = "PhoneNum 4",
            FormId = 0,
            EmployeesNumber = 4,
            TotalArea = 4
        },
        new()
        {
            EnterpriseTypeId = 5,
            Name = "Enterprise 5",
            Address = "Address 5",
            PhoneNumber = "PhoneNum 5",
            FormId = 2,
            EmployeesNumber = 5,
            TotalArea = 5
        },
        new()
        {
            EnterpriseTypeId = 5,
            Name = "Enterprise 6",
            Address = "Address 6",
            PhoneNumber = "PhoneNum 6",
            FormId = 3,
            EmployeesNumber = 6,
            TotalArea = 6
        },
        new()
        {
            EnterpriseTypeId = 2,
            Name = "Enterprise 7",
            Address = "Address 7",
            PhoneNumber = "PhoneNum 7",
            FormId = 2,
            EmployeesNumber = 7,
            TotalArea = 7
        },
        new()
        {
            EnterpriseTypeId = 2,
            Name = "Enterprise 8",
            Address = "Address 8",
            PhoneNumber = "PhoneNum 8",
            FormId = 1,
            EmployeesNumber = 8,
            TotalArea = 8
        },
        new()
        {
            EnterpriseTypeId = 4,
            Name = "Enterprise 9",
            Address = "Address 9",
            PhoneNumber = "PhoneNum 9",
            FormId = 1,
            EmployeesNumber = 9,
            TotalArea = 9
        },
        new()
        {
            EnterpriseTypeId = 2,
            Name = "Enterprise 10",
            Address = "Address 10",
            PhoneNumber = "PhoneNum 10",
            FormId = 2,
            EmployeesNumber = 10,
            TotalArea = 10
        }
    ];

    public List<Supplier> Suppliers =
    [
        new() { Name = "Supplier 0" },
        new() { Name = "Supplier 1" },
        new() { Name = "Supplier 2" },
        new() { Name = "Supplier 3" },
        new() { Name = "Supplier 4" }
    ];


    public DataProvider()
    {
        Supplies[0].Supplier = Suppliers[0];
        Supplies[0].Enterprise = Enterprises[0];
        Supplies[1].Supplier = Suppliers[4];
        Supplies[1].Enterprise = Enterprises[0];
        Supplies[2].Supplier = Suppliers[0];
        Supplies[2].Enterprise = Enterprises[1];
        Supplies[3].Supplier = Suppliers[1];
        Supplies[3].Enterprise = Enterprises[1];
        Supplies[4].Supplier = Suppliers[2];
        Supplies[4].Enterprise = Enterprises[1];
        Supplies[5].Supplier = Suppliers[3];
        Supplies[5].Enterprise = Enterprises[1];
        Supplies[6].Supplier = Suppliers[4];
        Supplies[6].Enterprise = Enterprises[1];
        Supplies[7].Supplier = Suppliers[1];
        Supplies[7].Enterprise = Enterprises[5];
        Supplies[8].Supplier = Suppliers[1];
        Supplies[8].Enterprise = Enterprises[6];
        Supplies[9].Supplier = Suppliers[2];
        Supplies[9].Enterprise = Enterprises[7];
        Supplies[10].Supplier = Suppliers[3];
        Supplies[10].Enterprise = Enterprises[8];
        Supplies[11].Supplier = Suppliers[4];
        Supplies[11].Enterprise = Enterprises[9];

        Enterprises[0].Type = EnterpriseTypes[0];
        Enterprises[0].Form = FormsOfOwnership[3];
        Enterprises[0].Supplies = [Supplies[0], Supplies[1]];
        Enterprises[1].Type = EnterpriseTypes[0];
        Enterprises[1].Form = FormsOfOwnership[4];
        Enterprises[1].Supplies = [Supplies[2], Supplies[3], Supplies[4], Supplies[5],  Supplies[6]];
        Enterprises[2].Type = EnterpriseTypes[3];
        Enterprises[2].Form = FormsOfOwnership[1];
        Enterprises[3].Type = EnterpriseTypes[3];
        Enterprises[3].Form = FormsOfOwnership[0];
        Enterprises[4].Type = EnterpriseTypes[5];
        Enterprises[4].Form = FormsOfOwnership[2];
        Enterprises[5].Type = EnterpriseTypes[5];
        Enterprises[5].Form = FormsOfOwnership[3];
        Enterprises[5].Supplies = [Supplies[7]];
        Enterprises[6].Type = EnterpriseTypes[2];
        Enterprises[6].Form = FormsOfOwnership[2];
        Enterprises[6].Supplies = [Supplies[8]];
        Enterprises[7].Type = EnterpriseTypes[2];
        Enterprises[7].Form = FormsOfOwnership[1];
        Enterprises[7].Supplies = [Supplies[9]];
        Enterprises[8].Type = EnterpriseTypes[4];
        Enterprises[8].Form = FormsOfOwnership[1];
        Enterprises[8].Supplies = [Supplies[10]];
        Enterprises[9].Type = EnterpriseTypes[2];
        Enterprises[9].Form = FormsOfOwnership[2];
        Enterprises[9].Supplies = [Supplies[11]];
        
        Suppliers[0].Supplies = [Supplies[0], Supplies[2]];
        Suppliers[1].Supplies = [Supplies[3], Supplies[7], Supplies[8]];
        Suppliers[2].Supplies = [Supplies[4], Supplies[9]];
        Suppliers[3].Supplies = [Supplies[5], Supplies[10]];
        Suppliers[4].Supplies = [Supplies[1], Supplies[6], Supplies[11]];
    }
}