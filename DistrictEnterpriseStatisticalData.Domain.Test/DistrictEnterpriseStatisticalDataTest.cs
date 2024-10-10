using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Domain.Test;

public class DistrictEnterpriseStatisticalDataTest(TestDB testDb) : IClassFixture<TestDB>
{
    
    private readonly EnterpriseRepository _enterpriseRepository = testDb.EnterpriseRepository;
    
    private readonly SupplierRepository _supplierRepository = testDb.SupplierRepository;

    [Fact]
    public void ReturnAllEnterpriseData()
    {
        var enterprise = _enterpriseRepository.GetById(1);
        Assert.NotNull(enterprise);
        Assert.Equal(1, enterprise.RegistrationNumber);
        Assert.Equal(1, enterprise.EnterpriseTypeId);
        Assert.Equal(testDb.DataProvider.EnterpriseTypes[0], enterprise.Type);
        Assert.Equal("Enterprise 1", enterprise.Name);
        Assert.Equal("Address 1", enterprise.Address);
        Assert.Equal("PhoneNum 1", enterprise.PhoneNumber);
        Assert.Equal(1, enterprise.FormId);
        Assert.Equal(testDb.DataProvider.FormsOfOwnership[3], enterprise.Form);
        Assert.Equal(1, enterprise.EmployeesNumber);
        Assert.Equal(1, enterprise.TotalArea);
        Assert.Equal([testDb.DataProvider.Supplies[0], testDb.DataProvider.Supplies[1]], enterprise.Supplies);
    }

    [Fact]
    public void ReturnSupplyBetweenDates()
    {
        var date1 = new DateOnly(2020, 01, 02);
        var date2 = new DateOnly(2021, 01, 02);

        var suppliers = _supplierRepository.ReturnSupplyBetweenDates(date1, date2);
        Assert.Equal([testDb.DataProvider.Suppliers[2], testDb.DataProvider.Suppliers[3]], suppliers);
    }

    [Fact]
    public void ReturnEnterprisesCountForEachSupplier()
    {
        var suppliers = _supplierRepository.ReturnEnterprisesCountForEachSupplier().ToList();
        
        Assert.Equal(2, suppliers[0].enterprisesCount);
        Assert.Equal(3, suppliers[1].enterprisesCount);
        Assert.Equal(2, suppliers[2].enterprisesCount);
        Assert.Equal(3, suppliers[3].enterprisesCount);
        Assert.Equal(2, suppliers[4].enterprisesCount);
    }

    [Fact]
    public void ReturnSuppliersCountForTypeAndForm()
    {
        var groupedData = _supplierRepository.ReturnSuppliersCountForTypeAndForm().ToList();
        Assert.True(groupedData[0].type == testDb.DataProvider.EnterpriseTypes[0] &&
                    groupedData[0].form == testDb.DataProvider.FormsOfOwnership[3] &&
                    groupedData[0].supplierCount == 2);
        Assert.True(groupedData[1].type == testDb.DataProvider.EnterpriseTypes[0] &&
                    groupedData[1].form == testDb.DataProvider.FormsOfOwnership[4] &&
                    groupedData[1].supplierCount == 5);
        Assert.True(groupedData[2].type == testDb.DataProvider.EnterpriseTypes[5] &&
                    groupedData[2].form == testDb.DataProvider.FormsOfOwnership[3] &&
                    groupedData[2].supplierCount == 1);
        Assert.True(groupedData[3].type == testDb.DataProvider.EnterpriseTypes[2] &&
                    groupedData[3].form == testDb.DataProvider.FormsOfOwnership[2] &&
                    groupedData[3].supplierCount == 2);
        Assert.True(groupedData[4].type == testDb.DataProvider.EnterpriseTypes[2] &&
                    groupedData[4].form == testDb.DataProvider.FormsOfOwnership[1] &&
                    groupedData[4].supplierCount == 1);
        Assert.True(groupedData[5].type == testDb.DataProvider.EnterpriseTypes[4] &&
                    groupedData[5].form == testDb.DataProvider.FormsOfOwnership[1] &&
                    groupedData[5].supplierCount == 1);
        Assert.True(groupedData[6].type == testDb.DataProvider.EnterpriseTypes[3] &&
                    groupedData[6].form == testDb.DataProvider.FormsOfOwnership[1] &&
                    groupedData[6].supplierCount == 0);
    }

    [Fact]
    public void FiveMostSuppliedEnterprises()
    {
        var topEnterprises = _enterpriseRepository.FiveMostSuppliedEnterprises().ToList();
        Assert.Equal(testDb.DataProvider.Enterprises[1], topEnterprises[0]);
        Assert.Equal(testDb.DataProvider.Enterprises[0], topEnterprises[1]);
        Assert.Equal(testDb.DataProvider.Enterprises[6], topEnterprises[2]);
        Assert.Equal(testDb.DataProvider.Enterprises[7], topEnterprises[3]);
        Assert.Equal(testDb.DataProvider.Enterprises[9], topEnterprises[4]);
    }

    [Fact]
    public void MaxProvidedSuppliers()
    {
        var date1 = new DateOnly(2021, 01, 02);
        var date2 = new DateOnly(2022, 01, 02);

        var supplierWithMaxSupplies = _supplierRepository.MaxProvidedSuppliers(date1, date2).ToList();

        Assert.Equal(testDb.DataProvider.Suppliers[4], supplierWithMaxSupplies[0]);
        Assert.Equal(testDb.DataProvider.Suppliers[0], supplierWithMaxSupplies[1]);
        Assert.Equal(testDb.DataProvider.Suppliers[1], supplierWithMaxSupplies[2]);
        Assert.Equal(testDb.DataProvider.Suppliers[2], supplierWithMaxSupplies[3]);
        Assert.Equal(testDb.DataProvider.Suppliers[3], supplierWithMaxSupplies[4]);
    }
}