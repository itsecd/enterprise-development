using Museum.Domain;

namespace Museum.Api.Storage;

public class MuseumStore
{
    public List<Exhibition> Exhibitions { get; } = DataSeeder.Exhibitions;

    public List<Visitor> Visitors { get; } = DataSeeder.Visitors;

    public List<Tour> Tours { get; } = DataSeeder.Tours;

    public List<Ticket> Tickets { get; } = DataSeeder.Tickets;

    public MuseumStore()
    {
        foreach (var tour in Tours)
        {
            tour.Exhibitions = tour.Exhibitions
                .Select(e => Exhibitions.First(x => x.Id == e.Id))
                .ToList();
        }

        foreach (var ticket in Tickets)
        {
            ticket.Tour = Tours.First(t => t.Id == ticket.Tour.Id);
            ticket.Visitor = Visitors.First(v => v.Id == ticket.Visitor.Id);
        }
    }
}
