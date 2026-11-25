using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.SqlServer.Repositories;

public sealed class SpecialistRepository(SalonDbContext context)
    : SqlRepository<BeautySpecialist>(context), ISpecialistRepository
{
    public async Task<IReadOnlyList<BeautySpecialist>> GetByMinimumExperienceAsync(
        int years, CancellationToken cancellationToken = default)
    {
        return await ReadQuery
            .Where(item => item.WorkExperience >= years)
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    public override Task<BeautySpecialist> AddAsync(
        BeautySpecialist entity, CancellationToken cancellationToken = default)
    {
        NormalizePassport(entity);
        return base.AddAsync(entity, cancellationToken);
    }

    public override Task<bool> UpdateAsync(
        BeautySpecialist entity, CancellationToken cancellationToken = default)
    {
        NormalizePassport(entity);
        return base.UpdateAsync(entity, cancellationToken);
    }

    private static void NormalizePassport(BeautySpecialist entity)
    {
        entity.PassportNumber = string.IsNullOrWhiteSpace(entity.PassportNumber)
            ? null
            : entity.PassportNumber.Trim();
    }
}
