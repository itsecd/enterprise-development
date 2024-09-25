namespace MediaLibrary.Domain;

/// <summary>
/// Трек в альбоме
/// </summary>
public class Track
{
    /// <summary>
    ///  Идентификатор трека
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер трека в альбоме
    /// </summary>
    public required int Number { get; set; }

    /// <summary>
    /// Название трека
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Идентификатор альбома трека
    /// </summary>
    public required int AlbumId { get; set; }

    /// <summary>
    /// Продолжительность трека
    /// </summary>
    public required TimeSpan Time { get; set; }
}
