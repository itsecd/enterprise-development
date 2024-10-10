using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

/// <summary>
/// Класс для осуществления запросов к базе данных к таблице типов предприятий
/// </summary>
public class EnterpriseTypeRepository(DistrictDbContext districtDbContext)
{
    /// <summary>
    /// Получение всех типов предприятий
    /// </summary>
    public IEnumerable<EnterpriseType> GetAll()
    {
        return districtDbContext.EnterpriseType;
    }

    /// <summary>
    /// Получение типа по идентификатору
    /// </summary>
    public EnterpriseType? GetById(int id)
    {
        return districtDbContext.EnterpriseType.Find(id);
    }

    /// <summary>
    /// Создание типа предприятия
    /// </summary>
    public EnterpriseType Create(EnterpriseType type)
    {
        var newType = districtDbContext.EnterpriseType.Add(type);
        districtDbContext.SaveChanges();
        return newType.Entity;
    }

    /// <summary>
    /// Обновление информации о типе предприятия
    /// </summary>
    public EnterpriseType Update(EnterpriseType type)
    {
        var newType = districtDbContext.EnterpriseType.Update(type).Entity;
        districtDbContext.SaveChanges();
        return newType;
    }

    /// <summary>
    /// Удаление типа предприятия
    /// </summary>
    public void Delete(EnterpriseType type)
    {
        districtDbContext.EnterpriseType.Remove(type);
        districtDbContext.SaveChanges();
    }
}