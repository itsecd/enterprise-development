using Museum.Domain;

namespace Museum.Tests;

/// <summary>
/// Общие тестовые данные
/// </summary>
public class MuseumFixture
{
    private readonly DataSeeder _seeder = new();

    /// <summary>
    /// Список выставок
    /// </summary>
    public List<Exhibition> Exhibitions => _seeder.Exhibitions;

    /// <summary>
    /// Список посетителей
    /// </summary>
    public List<Visitor> Visitors => _seeder.Visitors;

    /// <summary>
    /// Список экскурсий
    /// </summary>
    public List<Tour> Tours => _seeder.Tours;

    /// <summary>
    /// Список билетов
    /// </summary>
    public List<Ticket> Tickets => _seeder.Tickets;
}
