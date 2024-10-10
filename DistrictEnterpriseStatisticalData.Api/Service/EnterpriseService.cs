using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

public class EnterpriseService(EnterpriseRepository repository, IMapper mapper)
{
    public IEnumerable<EnterpriseDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<EnterpriseDto>);
    }

    public EnterpriseDto? GetByRegistrationNumber(int id)
    {
        return mapper.Map<EnterpriseDto>(repository.GetById(id));
    }

    public EnterpriseDto Create(EnterpriseDto enterprise)
    {
        return mapper.Map<EnterpriseDto>(repository.Create(mapper.Map<Enterprise>(enterprise)));
    }

    public EnterpriseDto Update(EnterpriseDto enterprise)
    {
        return mapper.Map<EnterpriseDto>(repository.Update(mapper.Map<Enterprise>(enterprise)));
    }

    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }

    public IEnumerable<EnterpriseDto> FiveMostSuppliedEnterprises()
    {
        return repository.FiveMostSuppliedEnterprises().Select(mapper.Map<EnterpriseDto>);
    }
}