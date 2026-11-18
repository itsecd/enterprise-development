using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;

namespace FitnessClub.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// EF-репозиторий клиентов
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ClientRepository(FitnessClubDbContext context)
    : EfRepository<Client>(context), IClientRepository;
