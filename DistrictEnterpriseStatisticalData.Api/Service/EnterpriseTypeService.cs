using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

public class EnterpriseTypeService(EnterpriseTypeRepository repository, IMapper mapper)
{
    public IEnumerable<EnterpriseTypeDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<EnterpriseTypeDto>);
    }

    public EnterpriseTypeDto? GetById(int id)
    {
        return mapper.Map<EnterpriseTypeDto>(repository.GetById(id));
    }

    public EnterpriseTypeDto Create(EnterpriseTypeDto enterprise)
    {
        return mapper.Map<EnterpriseTypeDto>(repository.Create(mapper.Map<EnterpriseType>(enterprise)));
    }

    public EnterpriseTypeDto Update(EnterpriseTypeDto enterprise)
    {
        return mapper.Map<EnterpriseTypeDto>(repository.Update(mapper.Map<EnterpriseType>(enterprise)));
    }

    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }
}