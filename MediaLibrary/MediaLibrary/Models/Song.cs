namespace MediaLibrary.Domain.Models;

/// <summary>
/// List of songs: unique id, name, number in album, album's name, duration
/// </summary>
public class Song
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public required int SongId { get; set; }
    /// <summary>
    /// Song's name
    /// </summary>
    /// <example>Surfin' Bird</example>
    public required string Name { get; set; }
    /// <summary>
    /// Number of a song in an album
    /// </summary>
    /// <example>1</example>
    public required int NumberInAlbum { get; set; }
    /// <summary>
    /// Name of album containing this song
    /// </summary>
    /// <example>The Bird's The Word</example>
    public required string AlbumName { get; set; }
    /// <summary>
    /// Song's duration (HHMMSS)
    /// </summary>
    /// <example>(0, 2, 30)</example>
    public TimeSpan Duration { get; set; }
}
