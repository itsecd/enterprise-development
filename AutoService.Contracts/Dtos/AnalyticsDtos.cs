namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для результата аналитики по популярным работам.
/// </summary>
public class TopWorkReportDto
{
    /// <summary>
    /// Название работы.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Количество обращений.
    /// </summary>
    public int Count { get; set; }
}

/// <summary>
/// DTO для результата аналитики по клиентам механика.
/// </summary>
public class ClientMechanicReportDto
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}

/// <summary>
/// DTO для результата аналитики по повторным клиентам.
/// </summary>
public class RepeatClientReportDto
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public string FullName { get; set; } = string.Empty;
}

/// <summary>
/// DTO для результата аналитики по механикам.
/// </summary>
public class MechanicReportDto
{
    /// <summary>
    /// Идентификатор механика.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО механика.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Специализация механика.
    /// </summary>
    public string Specialization { get; set; } = string.Empty;
}