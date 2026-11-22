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
    public async Task EnsureUniqueAsync(
        string passportNumber,
        Person? personBeingUpdated = null,
        CancellationToken cancellationToken = default)
    {
        var clientExists = personBeingUpdated is Client currentClient
            ? await clientRepository.AnyAsync(
                client => client.PassportNumber == passportNumber &&
                    client.Id != currentClient.Id,
                cancellationToken)
            : await clientRepository.AnyAsync(
                client => client.PassportNumber == passportNumber,
                cancellationToken);
        if (clientExists)
        {
            logger.LogWarning("Такой паспорт уже существует");
            throw new ConflictException(
                $"Человек с таким паспортным номером уже существует.");
        }
        var trainerExists = personBeingUpdated is Trainer currentTrainer
            ? await trainerRepository.AnyAsync(
                trainer => trainer.PassportNumber == passportNumber &&
                    trainer.Id != currentTrainer.Id,
                cancellationToken)
            : await trainerRepository.AnyAsync(
                trainer => trainer.PassportNumber == passportNumber,
                cancellationToken);
        if (trainerExists)
        {
            logger.LogWarning("Такой паспорт уже существует");
            throw new ConflictException(
                $"Человек с таким паспортным номером уже существует.");
        }
        logger.LogInformation("Проверка уникальности паспорта пройдена успешно");
    }
}
