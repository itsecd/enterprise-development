using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>Репозиторий сущностей Trainer в PostgreSQL.</summary>
public class TrainerRepository(FitnessClubDbContext context)
    : EfRepository<Trainer>(context), ITrainerRepository;
