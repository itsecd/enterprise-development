using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>Репозиторий сущностей Client в PostgreSQL.</summary>
public class ClientRepository(FitnessClubDbContext context)
    : EfRepository<Client>(context), IClientRepository;
