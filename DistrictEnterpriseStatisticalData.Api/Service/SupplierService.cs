using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

public class SupplierService(SupplierRepository repository, IMapper mapper)
{
    public IEnumerable<SupplierDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<SupplierDto>);
    }

    public SupplierDto? GetById(int id)
    {
        return mapper.Map<SupplierDto>(repository.GetById(id));
    }

    public SupplierDto Create(SupplierCreateDto enterprise)
    {
        return mapper.Map<SupplierDto>(repository.Create(mapper.Map<Supplier>(enterprise)));
    }

    public SupplierDto Update(SupplierDto enterprise)
    {
        return mapper.Map<SupplierDto>(repository.Update(mapper.Map<Supplier>(enterprise)));
    }

    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }

    public IEnumerable<SupplierDto> ReturnSupplyBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        return repository.ReturnSupplyBetweenDates(startDate, endDate).Select(mapper.Map<SupplierDto>);
    }

    public IEnumerable<EnterpriseCountForSupplierDto> ReturnEnterprisesCountForEachSupplier()
    {
        return repository.ReturnEnterprisesCountForEachSupplier()
            .Select(row => new EnterpriseCountForSupplierDto
            {
                Supplier = mapper.Map<SupplierDto>(row.supplier),
                EnterpriseCount = row.enterprisesCount
            });
    }

    public IEnumerable<SupplierCountForTypeAndFormDto> ReturnSuppliersCountForTypeAndForm()
    {
        return repository.ReturnSuppliersCountForTypeAndForm()
            .Select(row => new SupplierCountForTypeAndFormDto
            {
                Form = row.form,
                Type = row.type,
                SupplierCount = row.supplierCount
            });
    }

    public IEnumerable<SupplierDto> MaxProvidedSuppliers(DateOnly startDate, DateOnly endDate)
    {
        return repository.MaxProvidedSuppliers(startDate, endDate).Select(mapper.Map<SupplierDto>);
    }
}