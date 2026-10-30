using FitnessGym.Domain.Data;
using FitnessGym.Domain.Entities;

namespace FitnessGym.Tests;

/// <summary>
/// Фикстура с тестовыми данными фитнес-клуба, разделяемая между тестами
/// </summary>
public class FitnessGymFixture
{
    /// <summary>
    /// Зафиксированный момент времени, относительно которого строятся тестовые данные
    /// </summary>
    public DateTime Now => DataSeeder.Now;

    /// <summary>
    /// Дата, соответствующая зафиксированному моменту времени
    /// </summary>
    public DateOnly Today => DataSeeder.Today;

    /// <summary>
    /// Справочник специализаций тренеров
    /// </summary>
    public List<Specialization> Specializations => DataSeeder.Specializations;

    /// <summary>
    /// Список клиентов фитнес-клуба
    /// </summary>
    public List<Client> Clients => DataSeeder.Clients;

    /// <summary>
    /// Список тренеров фитнес-клуба
    /// </summary>
    public List<Trainer> Trainers => DataSeeder.Trainers;

    /// <summary>
    /// Список записей клиентов на персональные занятия
    /// </summary>
    public List<TrainingSession> Sessions => DataSeeder.Sessions;
}