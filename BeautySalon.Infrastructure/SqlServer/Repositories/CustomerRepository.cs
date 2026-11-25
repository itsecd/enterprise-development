using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.SqlServer.Repositories;

public sealed class CustomerRepository(SalonDbContext context)
    : SqlRepository<Customer>(context), ICustomerRepository;
