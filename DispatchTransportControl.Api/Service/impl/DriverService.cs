using AutoMapper;
using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Repository;
using DispatchTransportControl.Api.Repository.impl;
using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Service.impl;

public class DriverService(IDriverRepository repository, IMapper mapper) : IDriverService
{
    public IEnumerable<DriverDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<DriverDto>);
    }

    public DriverDto? GetById(int id)
    {
        return mapper.Map<DriverDto>(repository.GetById(id));
    }

    public DriverDto Create(DriverCreateDto dto)
    {
        return mapper.Map<DriverDto>(repository.Create(mapper.Map<Driver>(dto)));
    }

    public DriverDto Update(DriverDto dto)
    {
        var driver = repository.GetById(dto.Id);
        if (driver == null)
        {
            throw new Exception("Driver not found");
        }

        driver.Name = dto.Name;
        driver.Surname = dto.Surname;
        driver.Patronymic = dto.Patronymic;
        driver.Passport = dto.Passport;
        driver.DriverLicense = dto.DriverLicense;
        driver.Address = dto.Address;
        driver.Phone = dto.Phone;

        return mapper.Map<DriverDto>(repository.Update(driver));
    }

    public void Delete(int id)
    {
        var driver = repository.GetById(id);
        if (driver != null)
        {
            repository.Delete(driver);
        }
    }

    public IEnumerable<DriverDto> GetAllByPeriod(TimePeriodDto dto)
    {
        return repository.GetAllByPeriod(dto.Start, dto.End).Select(mapper.Map<DriverDto>);
    }

    public IEnumerable<DriverTripCountDto> GetTop5DriversByTripCount()
    {
        return repository.GetTop5DriversByTripCount()
            .Select(obj => new DriverTripCountDto
            {
                Driver = mapper.Map<DriverDto>(obj.Driver),
                TripCount = obj.TripCount
            });
    }

    public IEnumerable<DriverTripStatsDto> GetDriverTripStats()
    {
        return repository.GetDriverTripStats()
            .Select(obj => new DriverTripStatsDto
            {
                Driver = mapper.Map<DriverDto>(obj.Key),
                TripCount = obj.Value.TripCount,
                AvgTime = obj.Value.AvgTime,
                MaxTime = obj.Value.MaxTime,
            });
    }
}