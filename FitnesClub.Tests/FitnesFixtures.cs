using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Tests;

/// <summary>
/// Тестовая фикстура, предоставляющая предопределенные наборы данных из <see cref="DataSeeder"/> для модульных тестов.
/// </summary>
public class FitnesFixtures
{
    /// <summary>
    /// Возвращает тестовый набор специализаций тренеров.
    /// </summary>
    public List<Specialization> Specializations => DataSeeder.Specializations;

    /// <summary>
    /// Возвращает тестовый набор тренеров с предустановленными связями.
    /// </summary>
    public List<Trainer> Trainers => DataSeeder.Trainers;

    /// <summary>
    /// Возвращает тестовый набор клиентов фитнес-клуба.
    /// </summary>
    public List<Member> Members => DataSeeder.Members;

    /// <summary>
    /// Возвращает тестовый набор бронирований занятий.
    /// </summary>
    public List<Booking> Bookings => DataSeeder.Bookings;

    /// <summary>
    /// Опорная дата и время, относительно которой строятся временные интервалы в тестах.
    /// </summary>
    public DateTime TestDate { get; } = new(2026, 9, 28, 12, 0, 0);
}