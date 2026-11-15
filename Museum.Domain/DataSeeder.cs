using Museum.Shared;

namespace Museum.Domain;

/// <summary>
/// Генерирует тестовые данные музея.
/// Все связи между сущностями (экскурсия - выставки, билет - экскурсия и посетитель)
/// выставляются здесь, поэтому сущности ссылаются на одни и те же объекты.
/// </summary>
public sealed class DataSeeder
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
    public List<Tour> Tours { get; }

    /// <summary>
    /// Список билетов
    /// </summary>
    public List<Ticket> Tickets { get; }

    /// <summary>
    /// Создаёт набор связанных между собой тестовых данных
    /// </summary>
    public DataSeeder()
    {
        Exhibitions = CreateExhibitions();
        Visitors = CreateVisitors();
        Tours = CreateTours(Exhibitions);
        Tickets = CreateTickets(Tours, Visitors);
    }

    private static List<Exhibition> CreateExhibitions() =>
    [
        new()
        {
            Id = 1,
            Name = "Барокко",
            Theme = ExhibitionTheme.Art,
            HallNumber = 1,
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 3, 1)
        },
        new()
        {
            Id = 2,
            Name = "Древний Египет",
            Theme = ExhibitionTheme.History,
            HallNumber = 2,
            StartDate = new DateTime(2025, 2, 1),
            EndDate = new DateTime(2025, 4, 1)
        },
        new()
        {
            Id = 3,
            Name = "Космос",
            Theme = ExhibitionTheme.Science,
            HallNumber = 3,
            StartDate = new DateTime(2025, 3, 1),
            EndDate = new DateTime(2025, 5, 1)
        },
        new()
        {
            Id = 4,
            Name = "Дикая природа",
            Theme = ExhibitionTheme.Nature,
            HallNumber = 4,
            StartDate = new DateTime(2025, 1, 15),
            EndDate = new DateTime(2025, 3, 15)
        },
        new()
        {
            Id = 5,
            Name = "Художники",
            Theme = ExhibitionTheme.Art,
            HallNumber = 5,
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 6, 1)
        },
        new()
        {
            Id = 6,
            Name = "18 век",
            Theme = ExhibitionTheme.History,
            HallNumber = 1,
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 7, 1)
        },
        new()
        {
            Id = 7,
            Name = "Средневековье",
            Theme = ExhibitionTheme.History,
            HallNumber = 2,
            StartDate = new DateTime(2025, 6, 1),
            EndDate = new DateTime(2025, 8, 1)
        },
        new()
        {
            Id = 8,
            Name = "Океаны",
            Theme = ExhibitionTheme.Nature,
            HallNumber = 3,
            StartDate = new DateTime(2025, 2, 15),
            EndDate = new DateTime(2025, 4, 15)
        },
        new()
        {
            Id = 9,
            Name = "Артефакты",
            Theme = ExhibitionTheme.History,
            HallNumber = 4,
            StartDate = new DateTime(2025, 3, 15),
            EndDate = new DateTime(2025, 5, 15)
        },
        new()
        {
            Id = 10,
            Name = "Древняя Греция",
            Theme = ExhibitionTheme.History,
            HallNumber = 5,
            StartDate = new DateTime(2025, 7, 1),
            EndDate = new DateTime(2025, 9, 1)
        },
        new()
        {
            Id = 11,
            Name = "Динозавры",
            Theme = ExhibitionTheme.Science,
            HallNumber = 1,
            StartDate = new DateTime(2025, 8, 1),
            EndDate = new DateTime(2025, 10, 1)
        }
    ];

    private static List<Visitor> CreateVisitors() =>
    [
        new()
        {
            Id = 1,
            FullName = "Иванов Иван Иванович",
            Phone = "+79001234567",
            BirthDate = new DateTime(1990, 5, 15)
        },
        new()
        {
            Id = 2,
            FullName = "Петрова Мария Сергеевна",
            Phone = "+79007654321",
            BirthDate = new DateTime(1985, 3, 22)
        },
        new()
        {
            Id = 3,
            FullName = "Сидоров Алексей Петрович",
            Phone = "+79009876543",
            BirthDate = new DateTime(2000, 7, 10)
        },
        new()
        {
            Id = 4,
            FullName = "Козлова Анна Дмитриевна",
            Phone = "+79001112233",
            BirthDate = new DateTime(1978, 11, 5)
        },
        new()
        {
            Id = 5,
            FullName = "Новиков Дмитрий Александрович",
            Phone = "+79004445566",
            BirthDate = new DateTime(1995, 1, 30)
        },
        new()
        {
            Id = 6,
            FullName = "Морозова Елена Владимировна",
            Phone = "+79007778899",
            BirthDate = new DateTime(1988, 6, 18)
        },
        new()
        {
            Id = 7,
            FullName = "Волков Сергей Николаевич",
            Phone = "+79002223344",
            BirthDate = new DateTime(2003, 9, 25)
        },
        new()
        {
            Id = 8,
            FullName = "Соколова Ольга Игоревна",
            Phone = "+79005556677",
            BirthDate = new DateTime(1975, 4, 12)
        },
        new()
        {
            Id = 9,
            FullName = "Баранова Вероника Алексеевна",
            Phone = "+79008889900",
            BirthDate = new DateTime(1992, 12, 8)
        },
        new()
        {
            Id = 10,
            FullName = "Степанова София Вячеславовна",
            Phone = "+79003334455",
            BirthDate = new DateTime(1983, 2, 28)
        },
        new()
        {
            Id = 11,
            FullName = "Астафьев Вадим Батькович",
            Phone = "+79006667788",
            BirthDate = new DateTime(1998, 8, 14)
        }
    ];

    private static List<Tour> CreateTours(List<Exhibition> exhibitions) =>
    [
        new()
        {
            Id = 1,
            Date = new DateTime(2025, 2, 10),
            StartTime = new TimeSpan(10, 0, 0),
            DurationMinutes = 90,
            Exhibitions = [exhibitions[0], exhibitions[1]]
        },
        new()
        {
            Id = 2,
            Date = new DateTime(2025, 2, 15),
            StartTime = new TimeSpan(14, 0, 0),
            DurationMinutes = 60,
            Exhibitions = [exhibitions[2]]
        },
        new()
        {
            Id = 3,
            Date = new DateTime(2025, 3, 5),
            StartTime = new TimeSpan(11, 0, 0),
            DurationMinutes = 120,
            Exhibitions = [exhibitions[3], exhibitions[4]]
        },
        new()
        {
            Id = 4,
            Date = new DateTime(2025, 3, 20),
            StartTime = new TimeSpan(15, 0, 0),
            DurationMinutes = 90,
            Exhibitions = [exhibitions[0]]
        },
        new()
        {
            Id = 5,
            Date = new DateTime(2025, 4, 1),
            StartTime = new TimeSpan(10, 0, 0),
            DurationMinutes = 60,
            Exhibitions = [exhibitions[5], exhibitions[6]]
        },
        new()
        {
            Id = 6,
            Date = new DateTime(2025, 4, 15),
            StartTime = new TimeSpan(13, 0, 0),
            DurationMinutes = 90,
            Exhibitions = [exhibitions[7]]
        },
        new()
        {
            Id = 7,
            Date = new DateTime(2025, 5, 10),
            StartTime = new TimeSpan(11, 0, 0),
            DurationMinutes = 60,
            Exhibitions = [exhibitions[8], exhibitions[9]]
        },
        new()
        {
            Id = 8,
            Date = new DateTime(2025, 5, 25),
            StartTime = new TimeSpan(14, 0, 0),
            DurationMinutes = 120,
            Exhibitions = [exhibitions[1]]
        },
        new()
        {
            Id = 9,
            Date = new DateTime(2025, 6, 5),
            StartTime = new TimeSpan(10, 0, 0),
            DurationMinutes = 90,
            Exhibitions = [exhibitions[2], exhibitions[3]]
        },
        new()
        {
            Id = 10,
            Date = new DateTime(2025, 6, 20),
            StartTime = new TimeSpan(15, 0, 0),
            DurationMinutes = 60,
            Exhibitions = [exhibitions[4]]
        },
        new()
        {
            Id = 11,
            Date = new DateTime(2025, 7, 1),
            StartTime = new TimeSpan(11, 0, 0),
            DurationMinutes = 90,
            Exhibitions = [exhibitions[5], exhibitions[10]]
        }
    ];

    private static List<Ticket> CreateTickets(List<Tour> tours, List<Visitor> visitors) =>
    [
        new()
        {
            Id = 1,
            Tour = tours[0],
            Visitor = visitors[0],
            Type = TicketType.Adult,
            Price = 500
        },
        new()
        {
            Id = 2,
            Tour = tours[0],
            Visitor = visitors[1],
            Type = TicketType.Concession,
            Price = 250
        },
        new()
        {
            Id = 3,
            Tour = tours[1],
            Visitor = visitors[2],
            Type = TicketType.Adult,
            Price = 500
        },
        new()
        {
            Id = 4,
            Tour = tours[1],
            Visitor = visitors[3],
            Type = TicketType.Concession,
            Price = 250
        },
        new()
        {
            Id = 5,
            Tour = tours[2],
            Visitor = visitors[4],
            Type = TicketType.Adult,
            Price = 600
        },
        new()
        {
            Id = 6,
            Tour = tours[3],
            Visitor = visitors[5],
            Type = TicketType.Adult,
            Price = 500
        },
        new()
        {
            Id = 7,
            Tour = tours[4],
            Visitor = visitors[6],
            Type = TicketType.Concession,
            Price = 300
        },
        new()
        {
            Id = 8,
            Tour = tours[5],
            Visitor = visitors[7],
            Type = TicketType.Adult,
            Price = 500
        },
        new()
        {
            Id = 9,
            Tour = tours[6],
            Visitor = visitors[8],
            Type = TicketType.Adult,
            Price = 600
        },
        new()
        {
            Id = 10,
            Tour = tours[7],
            Visitor = visitors[9],
            Type = TicketType.Concession,
            Price = 250
        },
        new()
        {
            Id = 11,
            Tour = tours[8],
            Visitor = visitors[10],
            Type = TicketType.Adult,
            Price = 500
        },
        new()
        {
            Id = 12,
            Tour = tours[9],
            Visitor = visitors[0],
            Type = TicketType.Concession,
            Price = 300
        }
    ];
}
