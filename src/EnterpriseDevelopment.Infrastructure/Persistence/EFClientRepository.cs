using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий клиентов на основе Entity Framework Core
/// </summary>
public class EfClientRepository(ApplicationDbContext context)
    : EfRepository<Client>(context), IClientRepository;
