using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>Репозиторий сущностей Hall в PostgreSQL.</summary>
public class HallRepository(FitnessClubDbContext context)
    : EfRepository<Hall>(context), IHallRepository;
