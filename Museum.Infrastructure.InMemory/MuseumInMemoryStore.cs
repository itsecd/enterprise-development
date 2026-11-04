using Museum.Domain;

namespace Museum.Infrastructure.InMemory;

/// <summary>
/// Хранит все данные музея в памяти
/// </summary>
public sealed class MuseumInMemoryStore
{
    /// <summary>
    /// Все выставки музея
    /// </summary>
    public List<Exhibition> Exhibitions { get; } = DataSeeder.Exhibitions;

    /// <summary>
    /// Все посетители музея
    /// </summary>
    public List<Visitor> Visitors { get; } = DataSeeder.Visitors;

    /// <summary>
    /// Все экскурсии музея
    /// </summary>
    public List<Tour> Tours { get; } = DataSeeder.Tours;

    /// <summary>
    /// Все билеты музея
    /// </summary>
    public List<Ticket> Tickets { get; } = DataSeeder.Tickets;

    /// <summary>
    /// Загружает начальные данные и связывает сущности между собой
    /// </summary>
    public MuseumInMemoryStore()
    {
        foreach (var tour in Tours)
        {
            tour.Exhibitions = tour.Exhibitions
                .Select(exhibition => Exhibitions.First(
                    item => item.Id == exhibition.Id))
                .ToList();
        }

        foreach (var ticket in Tickets)
        {
            ticket.Tour = Tours.First(
                tour => tour.Id == ticket.Tour.Id);

            ticket.Visitor = Visitors.First(
                visitor => visitor.Id == ticket.Visitor.Id);
        }
    }
}
