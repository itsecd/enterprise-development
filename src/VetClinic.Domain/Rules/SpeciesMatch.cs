using VetClinic.Domain.Entities;
using VetClinic.Domain.Exceptions;

namespace VetClinic.Domain.Rules;

/// <summary>
/// Правило предметной области: вид питомца должен совпадать со специализацией врача.
/// Не зависит от способа хранения и используется и in-memory, и будущей реализацией на БД.
/// </summary>
public static class SpeciesMatch
{
    public static void Ensure(Pet pet, Veterinarian veterinarian)
    {
        if (pet.Species != veterinarian.Specialization.Species)
        {
            throw new DomainRuleException("Вид питомца не совпадает со специализацией врача.");
        }
    }
}
