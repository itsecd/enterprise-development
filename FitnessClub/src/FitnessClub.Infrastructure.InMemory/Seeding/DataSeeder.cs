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
    public async Task DataSeedingAsync(SeedData data, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Начало заполнения базы данных начальными данными");
        foreach(var specialization in data.Specializations) await specializationRepository.AddAsync(specialization, cancellationToken);
        foreach(var hall in data.Halls) await hallRepository.AddAsync(hall, cancellationToken);
        foreach(var client in data.Clients) await clientRepository.AddAsync(client, cancellationToken);
        foreach(var trainer in data.Trainers) await trainerRepository.AddAsync(trainer, cancellationToken);
        foreach(var booking in data.Bookings) await bookingRepository.AddAsync(booking, cancellationToken);
        logger.LogInformation("Заполнение базы данных начальными данными завершено");
    }
}