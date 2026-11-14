namespace BeautySalon.Contracts.Analytics;

/// <summary>
/// Ответ со списком мастеров и их стажем.
/// </summary>
public class MastersWithExperienceResponse
{
    /// <summary>
    /// Минимальный запрошенный стаж.
    /// </summary>
    public int MinYears { get; set; }
    /// <summary>
    /// Количество найденных мастеров.
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Список мастеров.
    /// </summary>
    public IEnumerable<MasterExperienceItem> Masters { get; set; } = [];
}

/// <summary>
/// Информация о мастере для аналитики.
/// </summary>
public class MasterExperienceItem
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Specialization { get; set; }
    public int ExperienceYears { get; set; }
}

/// <summary>
/// Ответ с информацией об окошках мастера.
/// </summary>
public class MasterGapsResponse
{
    public int MasterId { get; set; }
    public required string MasterName { get; set; }
    public int GapsCount { get; set; }
    public string? Message { get; set; }
    public IEnumerable<GapItem> Gaps { get; set; } = [];
}

/// <summary>
/// Информация о временном промежутке.
/// </summary>
public class GapItem
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int DurationMinutes { get; set; }
}

/// <summary>
/// Информация о популярной услуге.
/// </summary>
public class TopServiceItem
{
    public int ServiceId { get; set; }
    public required string ServiceName { get; set; }
    public required string Category { get; set; }
    public int AppointmentCount { get; set; }
}

/// <summary>
/// Ответ со списком клиентов с повторными записями.
/// </summary>
public class RepeatClientsResponse
{
    public int PeriodDays { get; set; }
    public int ClientsWithRepeats { get; set; }
    public IEnumerable<RepeatClientItem> Details { get; set; } = [];
}

/// <summary>
/// Информация о повторных записях клиента.
/// </summary>
public class RepeatClientItem
{
    public int ClientId { get; set; }
    public required string ClientName { get; set; }
    public int RepeatCount { get; set; }
}

/// <summary>
/// Ответ со списком клиентов, ходящих к нескольким мастерам.
/// </summary>
public class ClientsMultipleMastersResponse
{
    public int Count { get; set; }
    public IEnumerable<ClientMultipleMastersItem> Clients { get; set; } = [];
}

/// <summary>
/// Информация о клиенте у нескольких мастеров.
/// </summary>
public class ClientMultipleMastersItem
{
    public int ClientId { get; set; }
    public required string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public int UniqueMastersCount { get; set; }
}