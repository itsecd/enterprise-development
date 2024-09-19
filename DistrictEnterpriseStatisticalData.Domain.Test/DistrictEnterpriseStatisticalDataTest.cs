namespace DistrictEnterpriseStatisticalData.Domain.Test;

public class DistrictEnterpriseStatisticalDataTest(DataProvider dataProvider) : IClassFixture<DataProvider>
{
    private readonly DataProvider _dataProvider = dataProvider;

    [Fact]
    public void ReturnAllEnterpriseData()
    {
        Enterprise enterprise = _dataProvider.Enterprises.First();
        Assert.Equal(1, enterprise.RegistrationNumber);
        Assert.Equal(EnterpriseType.Agriculture, enterprise.Type);
        Assert.Equal("Enterprise 1", enterprise.Name);
        Assert.Equal("Address 1", enterprise.Address);
        Assert.Equal("PhoneNumber 1", enterprise.PhoneNumber);
        Assert.Equal(FormOfOwnership.Private, enterprise.Form);
        Assert.Equal(1, enterprise.EmployeesNumber);
        Assert.Equal(1, enterprise.TotalArea);
        Assert.Equal([_dataProvider.Suppliers[0], _dataProvider.Suppliers[4]], enterprise.Suppliers);
    }

    [Fact]
    public void ReturnSupplyBetweenDates()
    {
        DateOnly date1 = DateOnly.ParseExact("2020-01-02", "yyyy-mm-dd");
        DateOnly date2 = DateOnly.ParseExact("2021-01-02", "yyyy-mm-dd");

        var suppliers = _dataProvider.Suppliers
            .Where(s => s.Supplies.Any(s => s.Date >= date1 && s.Date <= date2))
            .OrderBy(s => s.Name).ToList();
        Assert.Equal([_dataProvider.Suppliers[2], _dataProvider.Suppliers[3]], suppliers);
    }

    [Fact]
    public void ReturnEnterpriseWithAllSuppliers()
    {
        var enterprises = _dataProvider.Enterprises
            .Where(e => e.Suppliers.Count == _dataProvider.Suppliers.Count).ToList();
        Assert.Equal([_dataProvider.Enterprises[1]], enterprises);
    }

    [Fact]
    public void ReturnSuppliersCountForTypeAndForm()
    {
        var groupedData = _dataProvider.Enterprises.SelectMany(enterprise => enterprise.Suppliers, 
                (enterprise, supplier) => new { enterprise.Type, enterprise.Form, supplier })
            .GroupBy(e => new { e.Type, e.Form })
            .Select(group => new
            {
                IndustryType = group.Key.Type,
                OwnershipForm = group.Key.Form,
                SupplierCount = group.Select(e => e.supplier).Distinct().Count()
            })
            .ToList();
        Assert.True(groupedData[0].IndustryType == EnterpriseType.Agriculture &&
                    groupedData[0].OwnershipForm == FormOfOwnership.Private &&
                    groupedData[0].SupplierCount == 2);
        Assert.True(groupedData[1].IndustryType == EnterpriseType.Agriculture &&
                    groupedData[1].OwnershipForm == FormOfOwnership.JointStock &&
                    groupedData[1].SupplierCount == 5);
        Assert.True(groupedData[2].IndustryType == EnterpriseType.MaterialAndTechnicalSupply &&
                    groupedData[2].OwnershipForm == FormOfOwnership.Private &&
                    groupedData[2].SupplierCount == 1);
        Assert.True(groupedData[3].IndustryType == EnterpriseType.LightIndustry &&
                    groupedData[3].OwnershipForm == FormOfOwnership.TOO &&
                    groupedData[3].SupplierCount == 1);
        Assert.True(groupedData[4].IndustryType == EnterpriseType.LightIndustry &&
                    groupedData[4].OwnershipForm == FormOfOwnership.MunicipalUrban &&
                    groupedData[4].SupplierCount == 1);
        Assert.True(groupedData[5].IndustryType == EnterpriseType.Building &&
                    groupedData[5].OwnershipForm == FormOfOwnership.MunicipalUrban &&
                    groupedData[5].SupplierCount == 1);
        Assert.True(groupedData[6].IndustryType == EnterpriseType.Transport &&
                    groupedData[6].OwnershipForm == FormOfOwnership.TOO &&
                    groupedData[6].SupplierCount == 1);
    }

    [Fact]
    public void FiveMostSuppliedEnterprises()
    {
        var topEnterprises = _dataProvider.Enterprises
            .OrderByDescending(e => e.Suppliers.SelectMany(s => s.Supplies).Count())
            .Take(5)
            .ToList();
        Assert.Equal(_dataProvider.Enterprises[1], topEnterprises[0]);
        Assert.Equal(_dataProvider.Enterprises[8], topEnterprises[1]);
        Assert.Equal(_dataProvider.Enterprises[0], topEnterprises[2]);
        Assert.Equal(_dataProvider.Enterprises[7], topEnterprises[3]);
        Assert.Equal(_dataProvider.Enterprises[5], topEnterprises[4]);
    }

    [Fact]
    public void MaxProvidedSuppliers()
    {
        DateOnly date1 = DateOnly.ParseExact("2021-01-02", "yyyy-mm-dd");
        DateOnly date2 = DateOnly.ParseExact("2022-01-02", "yyyy-mm-dd");
        
        var supplierWithMaxSupplies = _dataProvider.Suppliers
            .OrderByDescending(s => s.Supplies
                .Where(supply => supply.Date >= date1 && supply.Date <= date2)
                .Sum(supply => supply.Quantity))
            .ToList();
        
        Assert.Equal(_dataProvider.Suppliers[2], supplierWithMaxSupplies[0]);
        Assert.Equal(_dataProvider.Suppliers[4], supplierWithMaxSupplies[1]);
        Assert.Equal(_dataProvider.Suppliers[0], supplierWithMaxSupplies[2]);
        Assert.Equal(_dataProvider.Suppliers[1], supplierWithMaxSupplies[3]);
        Assert.Equal(_dataProvider.Suppliers[3], supplierWithMaxSupplies[4]);
    }
    
    
}