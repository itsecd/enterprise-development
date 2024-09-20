namespace MediaLibrary.Domain.Models;

/// <summary>
/// List of genres: unique id, title, list of artists in this genre
/// </summary>
public class Genre
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public int GenreId { get; set; }
    /// <summary>
    /// Genre's Title
    /// </summary>
    /// <example>Rock</example>
    public required string Title { get; set; }
    /// <summary>
    /// List of artist Ids in this genre
    /// </summary>
    /// <example>1, 2</example>
    public List<int> ArtistIds { get; set; } = new();
}
