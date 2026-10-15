using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;
        
namespace FitnesClub.Tests;

public class FitnesFixtures
{
    // Берем данные напрямую из твоего сидера
    public List<Specialization> Specializations => DataSeeder.Specializations;
    public List<Trainer> Trainers => DataSeeder.Trainers;
    public List<Member> Members => DataSeeder.Members;
    public List<Booking> Bookings => DataSeeder.Bookings;

    // Фиксированная дата для воспроизводимости тестов (сентябрь 2026)
    public DateTime TestDate { get; } = new(2026, 9, 28, 12, 0, 0);
}