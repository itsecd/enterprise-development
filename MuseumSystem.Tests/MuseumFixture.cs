using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Tests;

/// <summary>
/// Фикстура с тестовыми данными музея (один раз на класс тестов)
/// </summary>
public class MuseumFixture
{
    /// <summary>
    /// Список выставок
    /// </summary>
    public List<Exhibition> Exhibitions { get; }

    /// <summary>
    /// Список посетителей
    /// </summary>
    public List<Visitor> Visitors { get; }

    /// <summary>
    /// Список экскурсий
    /// </summary>
    public List<Excursion> Excursions { get; }

    /// <summary>
    /// Список билетов
    /// </summary>
    public List<Ticket> Tickets { get; }

    /// <summary>
    /// Инициализирует фикстуру тестовыми данными
    /// </summary>
    public MuseumFixture()
    {
        Exhibitions = MuseumDataSeeder.GetExhibitions();
        Visitors = MuseumDataSeeder.GetVisitors();
        Excursions = MuseumDataSeeder.GetExcursions(Exhibitions);
        Tickets = MuseumDataSeeder.GetTickets(Excursions, Visitors);
    }
}