using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

public class FormOfOwnershipRepository(DistrictDbContext districtDbContext)
{
    public IEnumerable<FormOfOwnership> GetAll()
    {
        return districtDbContext.FormOfOwnership;
    }

    public FormOfOwnership? GetById(int id)
    {
        return districtDbContext.FormOfOwnership.Find(id);
    }

    public FormOfOwnership Create(FormOfOwnership form)
    {
        var newForm = districtDbContext.FormOfOwnership.Add(form);
        districtDbContext.SaveChanges();
        return newForm.Entity;
    }

    public FormOfOwnership Update(FormOfOwnership form)
    {
        var newForm = districtDbContext.FormOfOwnership.Update(form).Entity;
        districtDbContext.SaveChanges();
        return newForm;
    }

    public void Delete(FormOfOwnership form)
    {
        districtDbContext.FormOfOwnership.Remove(form);
        districtDbContext.SaveChanges();
    }
}