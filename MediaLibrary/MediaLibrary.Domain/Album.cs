namespace MediaLibrary.Domain;

/// <summary>
/// Альбом исполнителя
/// </summary>
public class Album
{
    /// <summary>
    /// Идентификатор альбома
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор исполнителя
    /// </summary>
    public required int ActorId { get; set; }

    /// <summary>
    /// Название Альбома
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Дата релиза
    /// </summary>
    public required DateTime Date { get; set; }
}
