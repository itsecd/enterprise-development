using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

public class SupplyService(SupplyRepository repository, IMapper mapper)
{
    public IEnumerable<SupplyDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<SupplyDto>);
    }

    public SupplyDto? GetById(int id)
    {
        return mapper.Map<SupplyDto>(repository.GetById(id));
    }

    public SupplyDto Create(SupplyCreateDto enterprise)
    {
        return mapper.Map<SupplyDto>(repository.Create(mapper.Map<Supply>(enterprise)));
    }

    public SupplyDto Update(SupplyDto enterprise)
    {
        return mapper.Map<SupplyDto>(repository.Update(mapper.Map<Supply>(enterprise)));
    }

    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }
}