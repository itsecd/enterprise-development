namespace MediaLibrary.Tests;

/// <summary>
/// Unit тестирование с использованием данных из созданной медиатеки
/// </summary>
/// <param name="fixture"></param>
public class MediaLibraryTests(MediaFixture fixture) : IClassFixture<MediaFixture>
{
    /// <summary>
    /// Доступ к тестовым данным из медиатеки
    /// </summary>
    private readonly MediaFixture _fixture = fixture;

    /// <summary>
    /// Проверка вывода всех артистов
    /// </summary>
    [Fact]
    public void TestDisplayAllArtists()
    {
        var artists = _fixture.TestData.Artists.ToList();
        Assert.NotEmpty(artists);
        Assert.Equal(5, artists.Count);
    }

    /// <summary>
    /// Проверка вывода треков для указанного альбома, отсортированных по номеру
    /// </summary>
    [Fact]
    public void TestDisplayTracksInAlbum()
    {
        int albumId = 4;
        var tracks = _fixture.TestData.Tracks
            .Where(t => t.IdAlbum == albumId)
            .OrderBy(t => t.TrackNum)
            .ToList();
        Assert.NotEmpty(tracks);
        var expectedTracks = new List<string> { "Born", "LIII" };
        foreach (var track in tracks)
        {
            Assert.Contains(track.Title, expectedTracks);
        }
        Assert.Equal(2, tracks.Count);
    }

    /// <summary>
    /// Проверка вывода альбомов, выпущенных в указанном году, и количество треков в нем
    /// </summary>
    [Fact]
    public void TestDisplayAlbumsWithTrackCountInYear()
    {
        int year = 2020;
        var albums = _fixture.TestData.Albums
            .Where(a => a.Release.Year == year)
            .Select(a => new
            {
                Album = a,
                TrackCount = _fixture.TestData.Tracks.Count(t => t.IdAlbum == a.Id)
            })
            .ToList();
        Assert.NotEmpty(albums);
        var expectedTrackCounts = new Dictionary<int, int>
        {
            { 1, 1 },
            { 4, 2 }
        };
        foreach (var album in albums)
        {
            Assert.True(expectedTrackCounts.ContainsKey(album.Album.Id) 
                && expectedTrackCounts[album.Album.Id] == album.TrackCount);
        }
    }

    /// <summary>
    /// Проверка вывода топ-5 альбомов по продолжительности
    /// </summary>
    [Fact]
    public void TestDisplayTop5AlbumsByDuration()
    {
        var albums = _fixture.TestData.Albums
            .Select(a => new
            {
                Album = a,
                TotalDuration = _fixture.TestData.Tracks
                    .Where(t => t.IdAlbum == a.Id)
                    .Sum(t => t.Duration.TotalSeconds)
            })
            .OrderByDescending(a => a.TotalDuration)
            .Take(5)
            .ToList();
        Assert.NotEmpty(albums);
        Assert.Equal(5, albums.Count);
        Assert.True(albums.First().TotalDuration >= albums.Last().TotalDuration);
        var expectedAlbums = new List<string> { "Lie", "Origins", "Summer", "After Hours", "Starboy" };
        foreach (var album in albums)
        {
            Assert.Contains(album.Album.Title, expectedAlbums);
        }
    }

    /// <summary>
    /// Проверка вывода артистов с максимальным количеством альбомов
    /// </summary>
    [Fact]
    public void TestDisplayArtistsWithMostAlbums()
    {
        var artistAlbumCounts = _fixture.TestData.Artists
            .Select(a => new
            {
                Artist = a,
                AlbumCount = _fixture.TestData.Albums.Count(al => al.IdArtist == a.Id)
            })
            .OrderByDescending(a => a.AlbumCount)
            .ToList();
        Assert.NotEmpty(artistAlbumCounts);
        var maxAlbumOfArtist = artistAlbumCounts.First().AlbumCount;
        var topArtists = artistAlbumCounts
            .Where(a => a.AlbumCount == maxAlbumOfArtist)
            .Select(a => a.Artist)
            .ToList();
        Assert.NotEmpty(topArtists);
        var listTopArtists = new List<string> { "The Weekend", "Lana Del Rey" };
        foreach (var artist in topArtists) {
            Assert.Contains(artist.Name, listTopArtists);
        }
    }

    /// <summary>
    /// Проверка минимальной, средней и максимальной продолжительности альбомов.
    /// </summary>
    [Fact]
    public void TestDisplayAlbumDurationStats()
    {
        var albumDurations = _fixture.TestData.Albums
            .Select(a => _fixture.TestData.Tracks
            .Where(t => t.IdAlbum == a.Id)
            .Sum(t => t.Duration.TotalSeconds))
            .ToList();
        Assert.NotEmpty(albumDurations);
        var minDuration = albumDurations.Min();
        var avgDuration = albumDurations.Average();
        var maxDuration = albumDurations.Max();
        Assert.Equal(0, minDuration);
        Assert.Equal(887, maxDuration);
        Assert.Equal(322.7, avgDuration, 1);
    }
}