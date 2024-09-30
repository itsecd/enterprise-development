using EnterpriseStatistics.Domain;

namespace EnterpriseStatistics.Tests;

/// <summary>
/// Класс для тестирования
/// </summary>
public class EnterpriseStatisticsTests(EnterpriseStatisticsFixture fixture): IClassFixture<EnterpriseStatisticsFixture>
{
    private EnterpriseStatisticsFixture _fixture = fixture;

    /// <summary>
    /// Информация о конкретном предприятии по ОГРН
    /// </summary>
    [Fact]
    public void InfoSpecificEnterprise()
    {
        var specificEnterprise = _fixture.GetData()
            .Select(e => e.Enterprise)
            .Where(e => e.MainStateRegistrationNumber == 1234567890123).ToList();

        Assert.Equal("LLC \"AgroTech\"", specificEnterprise[0].Name);
        Assert.Equal("Moscow Pushkin St. 1", specificEnterprise[0].Address);
        Assert.Equal("+7 495 123-45-67", specificEnterprise[0].Phone);
        Assert.Equal(OwnershipForms.StateFederal, specificEnterprise[0].OwnershipForm);
        Assert.Equal(IndustryTypes.AgriculturalIndustry, specificEnterprise[0].IndustryType);
        Assert.Equal(50, specificEnterprise[0].EmployeeCount);
        Assert.Equal(1000, specificEnterprise[0].TotalArea);
    }

    /// <summary>
    /// Упорядоченные по ФИО поставщики, сделавшие поставку в конкретный период
    /// </summary>
    [Fact]
    public void InfoSupplierDate()
    {
        DateTime startDate = new DateTime(2024, 9, 1);
        DateTime endDate = new DateTime(2024, 9, 10);
       
        var supplierDate = _fixture.GetData()
            .Where(supply => supply.Date >= startDate && supply.Date <= endDate)
            .OrderBy(supply => supply.Supplier.FullName)
            .GroupBy(supply => supply.Supplier.FullName).ToList();

        Assert.True(supplierDate.Any());
        Assert.Equal("Anisimov Sergey Vladimirovich", supplierDate[0].Key);
        Assert.Equal("Bondar Oleg Ivanovich", supplierDate[1].Key);
        Assert.Equal("Sidorov Sidor Sidorovich", supplierDate[supplierDate.Count() -1].Key);
        Assert.True(supplierDate.Count() == 10);
    }

    /// <summary>
    /// Количество предприятий, с которыми работает каждый поставщик
    /// </summary>
    [Fact]
    public void CountEnterprise()
    {
        var supplier = _fixture.GetData()
            .Select(supply => supply.Supplier).Distinct().ToList();

        var supplierEnterpriseCounts = _fixture.GetData()
            .GroupBy(supply => supply.Supplier).Distinct() 
            .Select(tmp => new
            {
                SupplierId = tmp.Key.Id,
                FullName = tmp.Key.FullName,
                EnterpriseCount = tmp.Select(s => s.Enterprise.MainStateRegistrationNumber).Distinct().Count()
            }).ToList();
        
        Assert.True(supplierEnterpriseCounts.Any());
        Assert.Equal("Petrov Petr Petrovich", supplierEnterpriseCounts[1].FullName);
        Assert.Equal(3, supplierEnterpriseCounts[1].EnterpriseCount);
    }

    /// <summary>
    /// Количество предприятий, с которыми работает каждый поставщик
    /// </summary>
    [Fact]
    public void SupplierCountIndustryOwnership()
    {
        var result = _fixture.GetData()
        .GroupBy(supply => new { supply.Enterprise.IndustryType, supply.Enterprise.OwnershipForm })
        .Select(group => new
        {
            IndustryType = group.Key.IndustryType,
            OwnershipForm = group.Key.OwnershipForm,
            SupplierCount = group.Select(s => s.Supplier.Id).Distinct().Count()
        })
        .ToList();

        Assert.Equal(IndustryTypes.AgriculturalIndustry, result[1].IndustryType);
        Assert.Equal(OwnershipForms.Private, result[1].OwnershipForm);
        Assert.Equal(4, result[1].SupplierCount);
        Assert.True(result[2].SupplierCount == 3);
        Assert.Equal(IndustryTypes.Logistics, result[11].IndustryType);

    }

    /// <summary>
    /// Топ 5 предприятий по количеству поставок
    /// </summary>
    [Fact]
    public void Top5EnterprisesSupplyCount()
    {
        var topEnterprises = _fixture.GetData()
            .GroupBy(supply => supply.Enterprise) 
            .Select(group => new
            {
                Enterprise = group.Key, 
                SupplyCount = group.Count() 
            }).OrderByDescending(x => x.SupplyCount).Take(5).ToList();

        Assert.True(topEnterprises.Any());
        Assert.True(topEnterprises.Count() == 5);
        Assert.Equal(6, topEnterprises[0].SupplyCount);
        Assert.Equal("LLC \"AgroTech\"", topEnterprises[0].Enterprise.Name);
        Assert.Equal(5, topEnterprises[3].SupplyCount);
        Assert.Equal(1500, topEnterprises[3].Enterprise.TotalArea);

    }

    /// <summary>
    /// Поставщики с максимальным количеством товара за указанный период
    /// </summary>
        [Fact]
    public void MaxSupplierPeriod()
    {
        DateTime startDate = new DateTime(2024, 9, 1);
        DateTime endDate = new DateTime(2024, 9, 20);

        var supplierQuantities = _fixture.GetData()
            .Where(s => s.Date >= startDate && s.Date <= endDate) 
            .GroupBy(s => s.Supplier) 
            .Select(group => new
            {
                Supplier = group.Key, 
                TotalQuantity = group.Sum(s => s.Quanity) 
            })
            .ToList(); 

        
        var maxQuantity = supplierQuantities.Max(x => x.TotalQuantity);

        
        var suppliersWithMaxSupply = supplierQuantities
            .Where(x => x.TotalQuantity == maxQuantity)
            .ToList();

        Assert.True(suppliersWithMaxSupply.Any());
        Assert.True(suppliersWithMaxSupply.Count() == 2);
        Assert.True(maxQuantity == 2400);
        Assert.Equal(3, suppliersWithMaxSupply[0].Supplier.Id);
        Assert.Equal(4, suppliersWithMaxSupply[1].Supplier.Id);

    }


}