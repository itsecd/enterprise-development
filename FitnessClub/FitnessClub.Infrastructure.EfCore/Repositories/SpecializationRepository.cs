using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>Репозиторий сущностей Specialization в PostgreSQL.</summary>
public class SpecializationRepository(FitnessClubDbContext context)
    : EfRepository<Specialization>(context), ISpecializationRepository;
