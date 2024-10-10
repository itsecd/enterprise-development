using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

public class EnterpriseTypeRepository(DistrictDbContext districtDbContext)
{
    public IEnumerable<EnterpriseType> GetAll()
    {
        return districtDbContext.EnterpriseType;
    }

    public EnterpriseType? GetById(int id)
    {
        return districtDbContext.EnterpriseType.Find(id);
    }

    public EnterpriseType Create(EnterpriseType type)
    {
        var newType = districtDbContext.EnterpriseType.Add(type);
        districtDbContext.SaveChanges();
        return newType.Entity;
    }

    public EnterpriseType Update(EnterpriseType type)
    {
        var newType = districtDbContext.EnterpriseType.Update(type).Entity;
        districtDbContext.SaveChanges();
        return newType;
    }

    public void Delete(EnterpriseType type)
    {
        districtDbContext.EnterpriseType.Remove(type);
        districtDbContext.SaveChanges();
    }
}