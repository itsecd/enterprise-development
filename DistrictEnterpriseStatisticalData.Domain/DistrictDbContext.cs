using DistrictEnterpriseStatisticalData.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DistrictEnterpriseStatisticalData.Domain;

/// <summary>
/// Контекст базы данных
/// </summary>
/// <param name="options">Опции для подключения к базе данных</param>
public class DistrictDbContext(DbContextOptions<DistrictDbContext> options) : DbContext(options)
{
    public DbSet<Enterprise> Enterprise { get; set; }
    public DbSet<EnterpriseType> EnterpriseType { get; set; }
    public DbSet<FormOfOwnership> FormOfOwnership { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<Supply> Supply { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}