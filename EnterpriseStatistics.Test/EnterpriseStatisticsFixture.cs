using EnterpriseStatistics.Domain;

namespace EnterpriseStatistics.Tests;

/// <summary>
/// Подготовка тестовых данных
/// </summary>
public class EnterpriseStatisticsFixture
{
    public List<Supply> SupplyList;
    public EnterpriseStatisticsFixture()
    {
        SupplyList = EnterpriseStatisticsFileReader.ReadSupply(Path.Combine("Data","data.csv"));
    }
}
