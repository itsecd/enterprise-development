using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

public class EnterpriseRepository(DistrictDbContext districtDbContext)
{
    public IEnumerable<Enterprise> GetAll()
    {
        return districtDbContext.Enterprise;
    }

    public Enterprise? GetById(int id)
    {
        return districtDbContext.Enterprise.Find(id);
    }

    public Enterprise Create(Enterprise enterprise)
    {
        var newEnterprise = districtDbContext.Enterprise.Add(enterprise);
        districtDbContext.SaveChanges();
        return newEnterprise.Entity;
    }

    public Enterprise Update(Enterprise enterprise)
    {
        var newEnterprise = districtDbContext.Enterprise.Update(enterprise).Entity;
        districtDbContext.SaveChanges();
        return newEnterprise;
    }

    public void Delete(Enterprise enterprise)
    {
        districtDbContext.Enterprise.Remove(enterprise);
        districtDbContext.SaveChanges();
    }

    public IEnumerable<Enterprise> FiveMostSuppliedEnterprises()
    {
        return districtDbContext.Enterprise
            .OrderByDescending(e => e.Supplies.Count())
            .Take(5)
            .ToList();
    }
}