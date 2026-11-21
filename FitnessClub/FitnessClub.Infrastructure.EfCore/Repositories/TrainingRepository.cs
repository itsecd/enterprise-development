using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>Репозиторий сущностей Training в PostgreSQL.</summary>
public class TrainingRepository(FitnessClubDbContext context)
    : EfRepository<Training>(context), ITrainingRepository;
