namespace VetClinic.Domain.Models;

/// <summary>
/// Владелец и количество его питомцев
/// </summary>
public sealed record OwnerPetsCount(int Id, string FullName, int PetsCount);
