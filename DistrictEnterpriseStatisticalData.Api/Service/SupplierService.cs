using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;
using DistrictEnterpriseStatisticalData.Domain.Repository;

namespace DistrictEnterpriseStatisticalData.Api.Service;

/// <summary>
/// Сервис для поставщиков
/// </summary>
public class SupplierService(SupplierRepository repository, IMapper mapper)
{
    /// <summary>
    /// Получение всех поставщиков
    /// </summary>
    public IEnumerable<SupplierDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<SupplierDto>);
    }

    /// <summary>
    /// Получение поставщика по идентификатору
    /// </summary>
    public SupplierDto? GetById(int id)
    {
        return mapper.Map<SupplierDto>(repository.GetById(id));
    }

    /// <summary>
    /// Создание поставщика
    /// </summary>
    public SupplierDto Create(SupplierCreateDto enterprise)
    {
        return mapper.Map<SupplierDto>(repository.Create(mapper.Map<Supplier>(enterprise)));
    }

    /// <summary>
    /// Обновление информации о поставщике
    /// </summary>
    public SupplierDto Update(SupplierDto enterprise)
    {
        return mapper.Map<SupplierDto>(repository.Update(mapper.Map<Supplier>(enterprise)));
    }

    /// <summary>
    /// Удаление поставщика
    /// </summary>
    public void Delete(int id)
    {
        var enterprise = repository.GetById(id);
        if (enterprise != null)
            repository.Delete(enterprise);
    }

    /// <summary>
    /// Получение информации о всех поставщиках, поставивших сырье за заданных период, упорядоченных по названию
    /// </summary>
    public IEnumerable<SupplierDto> ReturnSupplyBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        return repository.ReturnSupplyBetweenDates(startDate, endDate).Select(mapper.Map<SupplierDto>);
    }

    /// <summary>
    /// Получение информации о количестве предприятий, с которым работает каждый поставщик
    /// </summary>
    public IEnumerable<EnterpriseCountForSupplierDto> ReturnEnterprisesCountForEachSupplier()
    {
        return repository.ReturnEnterprisesCountForEachSupplier()
            .Select(row => new EnterpriseCountForSupplierDto
            {
                Supplier = mapper.Map<SupplierDto>(row.supplier),
                EnterpriseCount = row.enterprisesCount
            });
    }

    /// <summary>
    /// Получение информации о количестве поставщиков для каждого типа отрасли и форме собственности
    /// </summary>
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

    /// <summary>
    /// Получение информации о поставщиках, поставивших максимальное количество товара за указанный период
    /// </summary>
    public IEnumerable<SupplierDto> MaxProvidedSuppliers(DateOnly startDate, DateOnly endDate)
    {
        return repository.MaxProvidedSuppliers(startDate, endDate).Select(mapper.Map<SupplierDto>);
    }
}