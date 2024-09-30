using AutoMapper;
using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Repository;
using DispatchTransportControl.Api.Repository.impl;
using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Service.impl;

public class VehicleService(
    IVehicleRepository repository,
    IVehicleModelRepository vehicleModelRepository,
    IMapper mapper
    ) : IVehicleService
{
    public IEnumerable<VehicleDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<VehicleDto>);
    }

    public VehicleDto? GetById(int id)
    {
        return mapper.Map<VehicleDto>(repository.GetById(id));
    }

    public VehicleDto Create(VehicleCreateDto dto)
    {
        var vehicleModel = vehicleModelRepository.GetById(dto.VehicleModelId);
        if (vehicleModel == null)
        {
            throw new Exception("Vehicle Model not found");
        }

        var vehicle = new Vehicle
        {
            RegistrationNumber = dto.RegistrationNumber,
            VehicleType = dto.VehicleType,
            VehicleModel = vehicleModel,
            YearOfManufacture = dto.YearOfManufacture
        };

        return mapper.Map<VehicleDto>(repository.Create(vehicle));
    }

    public VehicleDto Update(VehicleUpdateDto dto)
    {
        var vehicle = repository.GetById(dto.Id);
        if (vehicle == null)
        {
            throw new Exception("Vehicle not found");
        }
        var vehicleModel = vehicleModelRepository.GetById(dto.VehicleModelId);
        if (vehicleModel == null)
        {
            throw new Exception("Vehicle Model not found");
        }

        vehicle.RegistrationNumber = dto.RegistrationNumber;
        vehicle.VehicleType = dto.VehicleType;
        vehicle.VehicleModel = vehicleModel;
        vehicle.YearOfManufacture = dto.YearOfManufacture;

        return mapper.Map<VehicleDto>(repository.Update(vehicle));
    }

    public void Delete(int id)
    {
        var vehicle = repository.GetById(id);
        if (vehicle != null)
        {
            repository.Delete(vehicle);
        }
    }

    public IEnumerable<TripTimeForVehicleTypeAndModelDto> GetTotalTripTimeForEveryVehicleTypeAndModel()
    {
        return repository.GetTotalTripTimeForEveryVehicleTypeAndModel()
            .Select(keyValuePair => new TripTimeForVehicleTypeAndModelDto
            {
                VehicleType = keyValuePair.Key.VehicleType,
                VehicleModelDto = mapper.Map<VehicleModelDto>(keyValuePair.Key.VehicleModel),
                TotalTripTime = keyValuePair.Value
            })
            .ToList();
    }

    public IEnumerable<VehicleWithTripCountDto> GetVehiclesWithMaxTripsForPeriod(DateTime start, DateTime end)
    {
        return repository.GetVehiclesWithMaxTripsForPeriod(start, end)
            .Select(obj => new VehicleWithTripCountDto
            {
                Vehicle = mapper.Map<VehicleDto>(obj.Vehicle),
                TripCount = obj.TripCount
            });
    }
}