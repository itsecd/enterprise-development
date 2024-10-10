using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

public class FormOfOwnershipService(FormOfOwnershipRepository repository, IMapper mapper)
{
    public IEnumerable<FormOfOwnershipDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<FormOfOwnershipDto>);
    }

    public FormOfOwnershipDto? GetById(int id)
    {
        return mapper.Map<FormOfOwnershipDto>(repository.GetById(id));
    }

    public FormOfOwnershipDto Create(FormOfOwnershipCreateDto enterprise)
    {
        return mapper.Map<FormOfOwnershipDto>(repository.Create(mapper.Map<FormOfOwnership>(enterprise)));
    }

    public FormOfOwnershipDto Update(FormOfOwnershipDto enterprise)
    {
        return mapper.Map<FormOfOwnershipDto>(repository.Update(mapper.Map<FormOfOwnership>(enterprise)));
    }

    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }
}