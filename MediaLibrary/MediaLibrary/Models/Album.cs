namespace MediaLibrary.Domain.Models;

/// <summary>
/// List of albums: unique id, artist's Id, album's title, release date, list of song ids
/// </summary>
public class Album
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public required int AlbumId { get; set; }
    /// <summary>
    /// Artist's Id
    /// </summary>
    /// <example>1</example>
    public required int ArtistId { get; set; }
    /// <summary>
    /// Album's title
    /// </summary>
    /// <example>Nothing to see here</example>
    public required string Title { get; set; }
    /// <summary>
    /// Year of release
    /// </summary>
    /// <example>1984</example>
    public required int ReleaseDate { get; set; }
    /// <summary>
    /// List of songs in this album
    /// </summary>
    /// <example>1, 2</example>
    public required List<int> SongIds { get; set; } = new();
}
