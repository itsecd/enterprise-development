using EnterpriseStatistics.Domain;

namespace EnterpriseStatistics.Tests;

/// <summary>
/// Подготовка тестовых данных
/// </summary>
public class EnterpriseStatisticsFixture
{
    /// <summary>
    /// Список поставок
    /// </summary>
    private List<Supply>? _supplyList;

    /// <summary>
    /// Получение данных для тестов
    /// </summary>
    public List<Supply> GetData()
    {
        if(_supplyList != null) return _supplyList;
        string path = Path.Combine(Directory.GetCurrentDirectory(), "EnterpriseStatistics.Test", "data.csv");
        var suppliesReader = new EnterpriseStatisticsFileReader(path);
        _supplyList = suppliesReader.ReadSupply();
        return _supplyList;
    }

}
