using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

/// <summary>
/// Сервис для типов предприятий
/// </summary>
public class EnterpriseTypeService(EnterpriseTypeRepository repository, IMapper mapper)
{
    /// <summary>
    /// Получение всех типов предприятий
    /// </summary>
    public IEnumerable<EnterpriseTypeDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<EnterpriseTypeDto>);
    }

    /// <summary>
    /// Получение типа по идентификатору
    /// </summary>
    public EnterpriseTypeDto? GetById(int id)
    {
        return mapper.Map<EnterpriseTypeDto>(repository.GetById(id));
    }

    /// <summary>
    /// Создание типа предприятия
    /// </summary>
    public EnterpriseTypeDto Create(EnterpriseTypeCreateDto enterprise)
    {
        return mapper.Map<EnterpriseTypeDto>(repository.Create(mapper.Map<EnterpriseType>(enterprise)));
    }

    
    /// <summary>
    /// Обновление информации о типе предприятия
    /// </summary>
    public EnterpriseTypeDto Update(EnterpriseTypeDto enterprise)
    {
        return mapper.Map<EnterpriseTypeDto>(repository.Update(mapper.Map<EnterpriseType>(enterprise)));
    }

    /// <summary>
    /// Удаление типа предприятия
    /// </summary>
    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }
}