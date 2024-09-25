namespace MediaLibrary.Domain;

/// <summary>
/// Музыкальный исполнитель
/// </summary>
public class Actor
{
    /// <summary>
    /// Идентификатор исполнителя
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Имя исполнителя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание исполнителя 
    /// </summary>
    public required string Description { get; set; }
}
