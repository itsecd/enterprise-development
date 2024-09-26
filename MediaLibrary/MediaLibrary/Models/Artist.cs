namespace MediaLibrary.Domain.Models;

/// <summary>
/// List of artists: theyr names, descriptions, albums and genres
/// </summary>
public class Artist
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public required int ArtistId { get; set; }
    /// <summary>
    /// Artist's Name
    /// </summary>
    /// <example>The Stig</example>
    public required string Name { get; set; }
    /// <summary>
    /// Artist's Description
    /// </summary>
    /// <example>Нукоторые говорят, что он знает только два факта об утках. И оба неправильные...</example>
    public required string Description { get; set; }
    /// <summary>
    /// Artist's Album Ids
    /// </summary>
    /// <example>1, 2</example>
    public List<int> AlbumIds { get; set; } = new();
    /// <summary>
    /// Artist's Genre Ids
    /// </summary>
    /// <example>1, 2</example>
    public List<int> GenreIds { get; set; } = new();
}
