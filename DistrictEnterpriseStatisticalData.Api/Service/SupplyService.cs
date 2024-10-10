using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

/// <summary>
/// Сервия для поставок
/// </summary>
public class SupplyService(SupplyRepository repository, IMapper mapper)
{
    /// <summary>
    /// Получение всех поставок
    /// </summary>
    public IEnumerable<SupplyDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<SupplyDto>);
    }

    /// <summary>
    /// Получение поставки по идентификатору
    /// </summary>
    public SupplyDto? GetById(int id)
    {
        return mapper.Map<SupplyDto>(repository.GetById(id));
    }

    /// <summary>
    /// Создание поставки
    /// </summary>
    public SupplyDto Create(SupplyCreateDto enterprise)
    {
        return mapper.Map<SupplyDto>(repository.Create(mapper.Map<Supply>(enterprise)));
    }

    /// <summary>
    /// Обновление информации о поставке
    /// </summary>
    public SupplyDto Update(SupplyDto enterprise)
    {
        return mapper.Map<SupplyDto>(repository.Update(mapper.Map<Supply>(enterprise)));
    }

    /// <summary>
    /// Удаление поставки
    /// </summary>
    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }
}