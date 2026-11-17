using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.PostgreSql.Repositories;

public class PostgreSqlSpecializationRepository(FitnessGymDbContext context)
    : PostgreSqlRepository<Specialization>(context), ISpecializationRepository;
