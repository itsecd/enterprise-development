using FitnessClub.Application.Exceptions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Validation;

public class PassportNumberUniquenessValidator(
    IRepository<Client> clientRepository,
    IRepository<Trainer> trainerRepository,
    ILogger<PassportNumberUniquenessValidator> logger)
{
    public async Task EnsureUniqueAsync(string passportNumber, Person? personBeingUpdated = null)
    {
        var clients = await clientRepository.GetAllAsync();
        if (clients.Any(client =>
                client.PassportNumber == passportNumber &&
                (personBeingUpdated is not Client currentClient || client.Id != currentClient.Id)))
        {
            logger.LogWarning("Такой паспорт уже существует");
            throw new ConflictException(
                $"Человек с таким паспортным номером уже существует.");
        }
        var trainers = await trainerRepository.GetAllAsync();
        if (trainers.Any(trainer =>
                trainer.PassportNumber == passportNumber &&
                (personBeingUpdated is not Trainer currentTrainer || trainer.Id != currentTrainer.Id)))
        {
            logger.LogWarning("Такой паспорт уже существует");
            throw new ConflictException(
                $"Человек с таким паспортным номером уже существует.");
        }
        logger.LogInformation("Проверка уникальности паспорта пройдена успешно");
    }
}
