namespace MediaClasses;

/// <summary>
/// Связь между артистом и исполняемым жанром
/// </summary>
public class ParticipationArtistGenre
{
    /// <summary>
    /// Идентификатор связанного жанра музыки
    /// </summary>
    public required int IdGenre { get; set; }
    /// <summary>
    /// Идентификатор связанного музыкального исполнителя
    /// </summary>
    public required int IdArtist { get; set; }
}