using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

public class SupplyRepository(DistrictDbContext districtDbContext)
{
    public IEnumerable<Supply> GetAll()
    {
        return districtDbContext.Supply;
    }

    public Supply? GetById(int id)
    {
        return districtDbContext.Supply.Find(id);
    }

    public Supply Create(Supply supply)
    {
        var newSupplier = districtDbContext.Supply.Add(supply);
        districtDbContext.SaveChanges();
        return newSupplier.Entity;
    }

    public Supply Update(Supply supply)
    {
        var newSupply = districtDbContext.Supply.Update(supply).Entity;
        districtDbContext.SaveChanges();
        return newSupply;
    }

    public void Delete(Supply supply)
    {
        districtDbContext.Supply.Remove(supply);
        districtDbContext.SaveChanges();
    }
}