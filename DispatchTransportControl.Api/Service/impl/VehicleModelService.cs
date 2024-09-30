using AutoMapper;
using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Repository;
using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Service.impl;

public class VehicleModelService(IVehicleModelRepository repository, IMapper mapper) : IVehicleModelService
{
    public IEnumerable<VehicleModelDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<VehicleModelDto>);
    }

    public VehicleModelDto? GetById(int id)
    {
        return mapper.Map<VehicleModelDto>(repository.GetById(id));
    }

    public VehicleModelDto Create(VehicleModelCreateDto dto)
    {
        return mapper.Map<VehicleModelDto>(repository.Create(mapper.Map<VehicleModel>(dto)));
    }

    public VehicleModelDto Update(VehicleModelDto dto)
    {
        var vehicleModel = repository.GetById(dto.Id);
        if (vehicleModel == null)
        {
            throw new Exception("Vehicle Model not found");
        }

        vehicleModel.Name = dto.Name;
        vehicleModel.LowFloor = dto.LowFloor;
        vehicleModel.MaxCapacity = dto.MaxCapacity;

        return mapper.Map<VehicleModelDto>(repository.Update(vehicleModel));
    }

    public void Delete(int id)
    {
        var driver = repository.GetById(id);
        if (driver != null)
        {
            repository.Delete(driver);
        }
    }
}