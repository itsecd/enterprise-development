using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain.Repository;

public class SupplierRepository(DistrictDbContext districtDbContext)
{
    public IEnumerable<Supplier> GetAll()
    {
        return districtDbContext.Supplier;
    }

    public Supplier? GetById(int id)
    {
        return districtDbContext.Supplier.Find(id);
    }

    public Supplier Create(Supplier supplier)
    {
        var newSupplier = districtDbContext.Supplier.Add(supplier);
        districtDbContext.SaveChanges();
        return newSupplier.Entity;
    }

    public Supplier Update(Supplier supplier)
    {
        var newSupplier = districtDbContext.Supplier.Entry(supplier).Entity;
        districtDbContext.SaveChanges();
        return newSupplier;
    }

    public void Delete(Supplier supplier)
    {
        districtDbContext.Supplier.Remove(supplier);
        districtDbContext.SaveChanges();
    }

    public IEnumerable<Supplier> ReturnSupplyBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        var suppliers = districtDbContext.Supplier
            .Where(supplier => supplier.Supplies.Any(supply => supply.Date >= startDate && supply.Date <= endDate))
            .OrderBy(supplier => supplier.Name).ToList();
        return suppliers;
    }

    public IEnumerable<(Supplier supplier, int enterprisesCount)> ReturnEnterprisesCountForEachSupplier()
    {
        return districtDbContext.Supply
            .GroupBy(supply => supply.Supplier)
            .Select(group => new
            {
                supplier = group.Key,
                enterpriseCount = group.Count()
            })
            .AsEnumerable()
            .Select(supplier => (supplier.supplier, supplier.enterpriseCount));
    }
    
    public IEnumerable<(FormOfOwnership form, EnterpriseType type, int supplierCount)> ReturnSuppliersCountForTypeAndForm()
    {
        return districtDbContext.Enterprise
            .Join(
                districtDbContext.FormOfOwnership,
                enterprise => enterprise.FormId,
                form => form.Id,
                (enterprise, form) => new { Enterprise = enterprise, Form = form }
            )
            .Join(
                districtDbContext.EnterpriseType,
                enterpriseForm => enterpriseForm.Enterprise.EnterpriseTypeId,
                type => type.Id,
                (enterpriseForm, type) => new { enterpriseForm.Enterprise, enterpriseForm.Form, Type = type }
            )
            .AsEnumerable()
            .GroupBy(e => new { e.Form, e.Type })
            .Select(group => new
            {
                Form = group.Key.Form,
                Type = group.Key.Type,
                SupplierCount = group.SelectMany(e => e.Enterprise.Supplies.Select(s => s.Supplier)).Distinct().Count()
            })
            .AsEnumerable()
            .Select(s => (s.Form, s.Type, s.SupplierCount)).ToList();
    }

    public IEnumerable<Supplier> MaxProvidedSuppliers(DateOnly startDate, DateOnly endDate)
    {
        return districtDbContext.Supplier
            .OrderByDescending(s => s.Supplies
                .Where(supply => supply.Date >= startDate && supply.Date <= endDate)
                .Sum(supply => supply.Quantity));
    }
}