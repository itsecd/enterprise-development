using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

/// <summary>
/// Сервис для форм собственности
/// </summary>
public class FormOfOwnershipService(FormOfOwnershipRepository repository, IMapper mapper)
{
    /// <summary>
    /// Получение всех форм
    /// </summary>
    public IEnumerable<FormOfOwnershipDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<FormOfOwnershipDto>);
    }

    /// <summary>
    /// Получение формы по идентификатору
    /// </summary>
    public FormOfOwnershipDto? GetById(int id)
    {
        return mapper.Map<FormOfOwnershipDto>(repository.GetById(id));
    }

    /// <summary>
    /// Создание формы
    /// </summary>
    public FormOfOwnershipDto Create(FormOfOwnershipCreateDto enterprise)
    {
        return mapper.Map<FormOfOwnershipDto>(repository.Create(mapper.Map<FormOfOwnership>(enterprise)));
    }

    /// <summary>
    /// Обновление информации о форме
    /// </summary>
    public FormOfOwnershipDto Update(FormOfOwnershipDto enterprise)
    {
        return mapper.Map<FormOfOwnershipDto>(repository.Update(mapper.Map<FormOfOwnership>(enterprise)));
    }

    /// <summary>
    /// Удаление форме
    /// </summary>
    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }
}