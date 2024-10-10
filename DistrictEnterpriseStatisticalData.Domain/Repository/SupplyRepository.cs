using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

/// <summary>
/// Класс для осуществления запросов к базе данных к таблице поставок
/// </summary>
public class SupplyRepository(DistrictDbContext districtDbContext)
{
    /// <summary>
    /// Получение всех поставок
    /// </summary>
    public IEnumerable<Supply> GetAll()
    {
        return districtDbContext.Supply;
    }

    /// <summary>
    /// Получение поставки по идентификатору
    /// </summary>
    public Supply? GetById(int id)
    {
        return districtDbContext.Supply.Find(id);
    }

    /// <summary>
    /// Создание поставки
    /// </summary>
    public Supply Create(Supply supply)
    {
        var newSupplier = districtDbContext.Supply.Add(supply);
        districtDbContext.SaveChanges();
        return newSupplier.Entity;
    }

    /// <summary>
    /// Обновление информации о поставке
    /// </summary>
    public Supply Update(Supply supply)
    {
        var newSupply = districtDbContext.Supply.Update(supply).Entity;
        districtDbContext.SaveChanges();
        return newSupply;
    }

    /// <summary>
    /// Удаление поставки
    /// </summary>
    public void Delete(Supply supply)
    {
        districtDbContext.Supply.Remove(supply);
        districtDbContext.SaveChanges();
    }
}