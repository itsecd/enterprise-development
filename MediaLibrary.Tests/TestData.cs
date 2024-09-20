using MediaLibrary.Classes;

namespace MediaLibrary.Tests;

/// <summary>
/// Данные для тестов медиатеки
/// </summary>
public class TestData
{
    /// <summary>
    /// Список музыкальных исполнителей
    /// </summary>
    public List<Artist>? Artists { get; set; }
    /// <summary>
    /// Список музыкальных альбомов
    /// </summary>
    public List<Album>? Albums { get; set; }
    /// <summary>
    /// Список музыкальных треков
    /// </summary>
    public List<Track>? Tracks { get; set; }
    /// <summary>
    /// Список жанров музыки
    /// </summary>
    public List<Genre>? Genres { get; set; }
    /// <summary>
    /// Список связей артист и жанр музыки
    /// </summary>
    public List<ParticipationArtistGenre>? ParticipationArtistGenre { get; set; }
}