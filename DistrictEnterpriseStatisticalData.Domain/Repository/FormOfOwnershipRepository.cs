using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

/// <summary>
/// Класс для осуществления запросов к базе данных к таблице форм собственности
/// </summary>
public class FormOfOwnershipRepository(DistrictDbContext districtDbContext)
{
    /// <summary>
    /// Получение всех форм
    /// </summary>
    public IEnumerable<FormOfOwnership> GetAll()
    {
        return districtDbContext.FormOfOwnership;
    }

    /// <summary>
    /// Получение формы по идентификатору
    /// </summary>
    public FormOfOwnership? GetById(int id)
    {
        return districtDbContext.FormOfOwnership.Find(id);
    }

    /// <summary>
    /// Создание формы
    /// </summary>
    public FormOfOwnership Create(FormOfOwnership form)
    {
        var newForm = districtDbContext.FormOfOwnership.Add(form);
        districtDbContext.SaveChanges();
        return newForm.Entity;
    }

    /// <summary>
    /// Обновление информации о форме
    /// </summary>
    public FormOfOwnership Update(FormOfOwnership form)
    {
        var newForm = districtDbContext.FormOfOwnership.Update(form).Entity;
        districtDbContext.SaveChanges();
        return newForm;
    }

    /// <summary>
    /// Удаление форме
    /// </summary>
    public void Delete(FormOfOwnership form)
    {
        districtDbContext.FormOfOwnership.Remove(form);
        districtDbContext.SaveChanges();
    }
}