namespace FitnessGym.Domain.Entities;

/// <summary>
/// Общие ограничения строк для схемы базы данных и входных DTO.
/// </summary>
public static class TextLimits
{
    public const int PassportNumber = 20;
    public const int FullName = 200;
    public const int Phone = 32;
    public const int SpecializationName = 100;
    public const int HallName = 100;
}
