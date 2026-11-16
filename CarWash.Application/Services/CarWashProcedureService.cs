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
    public async Task<CarWashProcedureDto> Create(
        CarWashProcedureCreateUpdateDto dto)
    {
        var newId = (await procedureRepository.ReadAll())
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

        await procedureRepository.Create(procedure);

        return MapToDto(procedure);
    }

    public async Task<CarWashProcedureDto?> Get(int dtoId)
    {
        var procedure = await procedureRepository.Read(dtoId);

        return procedure is null ? null : MapToDto(procedure);
    }

    public async Task<IList<CarWashProcedureDto>> GetAll()
    {
        var procedures = await procedureRepository.ReadAll();

        return procedures
            .Select(MapToDto)
            .ToList();
    }

    public async Task<CarWashProcedureDto> Update(
        CarWashProcedureCreateUpdateDto dto,
        int dtoId)
    {
        var oldProcedure = await procedureRepository.Read(dtoId)
            ?? throw new KeyNotFoundException(
                $"Процедура с Id {dtoId} не найдена");

        var procedure = new CarWashProcedure
        {
            Id = dtoId,
            Name = dto.Name,
            CarCategory = dto.CarCategory,
            Price = dto.Price,
            Duration = dto.Duration,
            Orders = oldProcedure.Orders
        };

        var updatedProcedure = await procedureRepository.Update(procedure);

        return MapToDto(updatedProcedure);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await procedureRepository.Delete(dtoId);
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