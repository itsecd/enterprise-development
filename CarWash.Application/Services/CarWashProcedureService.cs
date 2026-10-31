using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Procedures;
using CarWash.Domain;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Служба для CRUD-операций над услугами автомойки
/// </summary>
public class CarWashProcedureService(
    IRepository<CarWashProcedure, int> procedureRepository)
    : IApplicationService<CarWashProcedureDto, CarWashProcedureCreateUpdateDto, int>
{
    public CarWashProcedureDto Create(CarWashProcedureCreateUpdateDto dto)
    {
        var newId = procedureRepository.ReadAll()
            .Select(procedure => procedure.Id)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var procedure = new CarWashProcedure
        {
            Id = newId,
            Name = dto.Name,
            CarCategory = dto.CarCategory,
            Price = dto.Price,
            Duration = dto.Duration
        };

        procedureRepository.Create(procedure);

        return MapToDto(procedure);
    }

    public CarWashProcedureDto Get(int dtoId)
    {
        return MapToDto(procedureRepository.Read(dtoId));
    }

    public List<CarWashProcedureDto> GetAll()
    {
        return procedureRepository.ReadAll()
            .Select(MapToDto)
            .ToList();
    }

    public CarWashProcedureDto Update(
        CarWashProcedureCreateUpdateDto dto,
        int dtoId)
    {
        var oldProcedure = procedureRepository.Read(dtoId);

        var procedure = new CarWashProcedure
        {
            Id = dtoId,
            Name = dto.Name,
            CarCategory = dto.CarCategory,
            Price = dto.Price,
            Duration = dto.Duration,
            Orders = oldProcedure.Orders
        };

        procedureRepository.Update(procedure);

        return MapToDto(procedure);
    }

    public void Delete(int dtoId)
    {
        procedureRepository.Delete(dtoId);
    }

    private static CarWashProcedureDto MapToDto(
        CarWashProcedure procedure)
    {
        return new CarWashProcedureDto(
            procedure.Id,
            procedure.Name,
            procedure.CarCategory,
            procedure.Price,
            procedure.Duration);
    }
}
