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
    /// <summary>
    /// Идентификатор мастера.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ФИО мастера.
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Специализация мастера.
    /// </summary>
    public required string Specialization { get; set; }
    /// <summary>
    /// Стаж работы мастера в годах.
    /// </summary>
    public int ExperienceYears { get; set; }
}

/// <summary>
/// Ответ с информацией об окошках мастера.
/// </summary>
public class MasterGapsResponse
{
    /// <summary>
    /// Идентификатор мастера.
    /// </summary>
    public int MasterId { get; set; }
    /// <summary>
    /// ФИО мастера.
    /// </summary>
    public required string MasterName { get; set; }
    /// <summary>
    /// Общее количество "окошек".
    /// </summary>
    public int GapsCount { get; set; }
    /// <summary>
    /// Информационное сообщение.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Список временных промежутков между записями мастера.
    /// </summary>
    public IEnumerable<GapItem> Gaps { get; set; } = [];
}

/// <summary>
/// Информация о временном промежутке.
/// </summary>
public class GapItem
{
    /// <summary>
    /// Время начала "окошка".
    /// </summary>
    public DateTime From { get; set; }
    /// <summary>
    /// Время окончания "окошка".
    /// </summary>
    public DateTime To { get; set; }
    /// <summary>
    /// Длительность "окошка".
    /// </summary>
    public int DurationMinutes { get; set; }
}

/// <summary>
/// Информация о популярной услуге.
/// </summary>
public class TopServiceItem
{
    /// <summary>
    /// Идентификатор услуги.
    /// </summary>
    public int ServiceId { get; set; }
    /// <summary>
    /// Название услуги.
    /// </summary>
    public required string ServiceName { get; set; }
    /// <summary>
    /// Категория услуги.
    /// </summary>
    public required string Category { get; set; }
    /// <summary>
    /// Стоимость услуги.
    /// </summary>
    public int AppointmentCount { get; set; }
}

/// <summary>
/// Ответ со списком клиентов с повторными записями.
/// </summary>
public class RepeatClientsResponse
{
    /// <summary>
    /// Количество дней в анализируемом периоде.
    /// </summary>
    public int PeriodDays { get; set; }
    /// <summary>
    /// Общее количество клиентов, имеющих более одной записи за период.
    /// </summary>
    public int ClientsWithRepeats { get; set; }
    /// <summary>
    /// Детальная информация по каждому клиенту с повторными записями.
    /// </summary>
    public IEnumerable<RepeatClientItem> Details { get; set; } = [];
}

/// <summary>
/// Информация о повторных записях клиента.
/// </summary>
public class RepeatClientItem
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }
    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public required string ClientName { get; set; }
    /// <summary>
    /// Количество повторных записей.
    /// </summary>
    public int RepeatCount { get; set; }
}

/// <summary>
/// Ответ со списком клиентов, ходящих к нескольким мастерам.
/// </summary>
public class ClientsMultipleMastersResponse
{
    /// <summary>
    /// Общее количество клиентов, записанных к нескольким мастерам.
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Список клиентов, записанных к нескольким мастерам, отсортированный по дате рождения.
    /// </summary>
    public IEnumerable<ClientMultipleMastersItem> Clients { get; set; } = [];
}

/// <summary>
/// Информация о клиенте у нескольких мастеров.
/// </summary>
public class ClientMultipleMastersItem
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }
    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    public DateTime BirthDate { get; set; }
    /// <summary>
    /// Количество различных мастеров у клиента.
    /// </summary>
    public int UniqueMastersCount { get; set; }
}