using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Data;

namespace MuseumSystem.Infrastructure.EfCore.Data;

public static class SeedData
{
	public static Exhibition[] Exhibitions => MuseumDataSeeder.GetExhibitions().ToArray();

	public static Visitor[] Visitors => MuseumDataSeeder.GetVisitors().ToArray();

	public static Excursion[] Excursions
	{
		get
		{
			var exhibitions = Exhibitions.ToDictionary(e => e.Id);
			var excursions = MuseumDataSeeder.GetExcursions(exhibitions.Values.ToList());

			// Очищаем навигационное свойство, дабы EF Core мог сидировать Excursion
			foreach (var excursion in excursions)
			{
				excursion.Exhibitions.Clear();
			}

			return excursions.ToArray();
		}
	}

	public static Ticket[] Tickets
	{
		get
		{
			var excursions = Excursions.ToDictionary(e => e.Id);
			var visitors = Visitors.ToDictionary(v => v.Id);
			var tickets = MuseumDataSeeder.GetTickets(excursions.Values.ToList(), visitors.Values.ToList());

			foreach (var ticket in tickets)
			{
				ticket.ExcursionId = ticket.Excursion.Id;
				ticket.VisitorId = ticket.Visitor.Id;

				// Очищаем навигационные свойства аналогично
				ticket.Excursion = null!;
				ticket.Visitor = null!;
			}

			return tickets.ToArray();
		}
	}

	public static ExcursionExhibition[] ExcursionExhibitions
	{
		get
		{
			// Получаем экскурсии с ещё не очищенными Exhibitions
			var exhibitions = Exhibitions.ToDictionary(e => e.Id);
			var rawExcursions = MuseumDataSeeder.GetExcursions(exhibitions.Values.ToList());

			var result = new List<ExcursionExhibition>();
			foreach (var excursion in rawExcursions)
			{
				foreach (var exhibition in excursion.Exhibitions)
				{
					result.Add(new ExcursionExhibition
					{
						ExcursionId = excursion.Id,
						ExhibitionId = exhibition.Id
					});
				}
			}
			return result.ToArray();
		}
	}
}