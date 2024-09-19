namespace MediaClasses;

/// <summary>
/// Музыкальный исполнитель
/// </summary>
public class Artist
{
    /// <summary>
    /// Идентификатор артиста
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Никнейм артиста
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Краткое описание артиста
    /// </summary>
    public required string Description { get; set; }
}