using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

/// <summary>
/// Класс для осуществления запросов к базе данных к таблице предприятий
/// </summary>
public class EnterpriseRepository(DistrictDbContext districtDbContext)
{
    /// <summary>
    /// Получение всех предприятий
    /// </summary>
    public IEnumerable<Enterprise> GetAll()
    {
        return districtDbContext.Enterprise;
    }

    /// <summary>
    /// Получение предприятия по регистрационному номеру
    /// </summary>
    public Enterprise? GetById(int id)
    {
        return districtDbContext.Enterprise.Find(id);
    }
    
    /// <summary>
    /// Создание предприятия
    /// </summary>
    public Enterprise Create(Enterprise enterprise)
    {
        var newEnterprise = districtDbContext.Enterprise.Add(enterprise);
        districtDbContext.SaveChanges();
        return newEnterprise.Entity;
    }

    /// <summary>
    /// Обновление информации о предприятии
    /// </summary>
    public Enterprise Update(Enterprise enterprise)
    {
        var newEnterprise = districtDbContext.Enterprise.Update(enterprise).Entity;
        districtDbContext.SaveChanges();
        return newEnterprise;
    }

    /// <summary>
    /// Удаление предприятия
    /// </summary>
    public void Delete(Enterprise enterprise)
    {
        districtDbContext.Enterprise.Remove(enterprise);
        districtDbContext.SaveChanges();
    }

    /// <summary>
    /// Получение топ 5 предприятий по количеству поставок
    /// </summary>
    public IEnumerable<Enterprise> FiveMostSuppliedEnterprises()
    {
        return districtDbContext.Enterprise
            .OrderByDescending(e => e.Supplies.Count())
            .Take(5)
            .ToList();
    }
}