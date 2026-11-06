using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Seeds;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Infrastructure.InMemory.Seeding;

public class DataSeeder(
    IRepository<Client> clientRepository,
    IRepository<Trainer> trainerRepository,
    IRepository<Hall> hallRepository,
    IRepository<Booking> bookingRepository,
    IRepository<Specialization> specializationRepository,
    ILogger<DataSeeder> logger)
{ 
    public async Task DataSeedingAsync(SeedData data)
    {
        logger.LogInformation("Начало заполнения базы данных начальными данными");
        foreach(var specialization in data.Specializations) await specializationRepository.AddAsync(specialization);
        foreach(var hall in data.Halls) await hallRepository.AddAsync(hall);
        foreach(var client in data.Clients) await clientRepository.AddAsync(client);
        foreach(var trainer in data.Trainers) await trainerRepository.AddAsync(trainer);
        foreach(var booking in data.Bookings) await bookingRepository.AddAsync(booking);
        logger.LogInformation("Заполнение базы данных начальными данными завершено");
    }
}