using MediaLibrary.Domain;
using System.Diagnostics;

namespace MediaLibrary.Tests;

/// <summary>
/// Класс для тестирования запросов
/// </summary>
/// <param name="fixture">Данные для тестирования</param>
public class MediaLibraryTest(MediaLibraryFixture fixture) : IClassFixture<MediaLibraryFixture>
{
    /// <summary>
    /// Проверка вывода всех исполнителей
    /// </summary>
    [Fact]
    public void AllActors()
    {
        var actorInfo =
            from Actor in fixture.GetActors()
            orderby Actor
            select Actor;
        Assert.NotNull(actorInfo);
        Assert.Equal(9, actorInfo.Count());
    }

    /// <summary>
    /// Проверка вывода всех треков в альбоме, упорядоченных по номеру
    /// </summary>
    [Fact]
    public void TracksInAlbum()
    {
        var tracksInfo =
            (from Album in fixture.GetAlbums()
            join Track in fixture.GetTracks() on Album.Id equals Track.AlbumId
            where Album.Name == "Lemonade"
            orderby Track.Number
            select Track).ToList();
        var expectedValues = new List<Track>()
        {
            new() { Id = 1, Number = 1, Name = "Pray You Catch Me",
                AlbumId = 1, Time = TimeSpan.FromSeconds(229) },
            new() { Id = 2, Number = 2, Name = "Hold Up",
                AlbumId = 1, Time = TimeSpan.FromSeconds(261) },
            new() { Id = 3, Number = 3, Name = "Sorry",
                AlbumId = 1, Time = TimeSpan.FromSeconds(224) },
            new() { Id = 4, Number = 4, Name = "Freedom",
                AlbumId = 1, Time = TimeSpan.FromSeconds(265) }
        };
        Assert.NotNull(tracksInfo);
        Assert.Equal(expectedValues[2].Name, tracksInfo[2].Name);
        for (var i = 0; i < expectedValues.Count; i++)
        {
            Assert.Equal(expectedValues[i].Number, tracksInfo[i].Number);
        }
    }

    /// <summary>
    /// Проверка вывода всех альбомов в указанный год с количеством треков в альбоме
    /// </summary>
    [Fact]
    public void AlbumsInfo()
    {
        var albumsInfo =
            (from Album in fixture.GetAlbums()
            where Album.Date.Year == 2022
            join Track in fixture.GetTracks()
            on Album.Id equals Track.AlbumId into albumTracks
            select new 
            {
                album = Album,
                tracksCount = albumTracks.Count(),
            }).ToList();
        Assert.NotNull(albumsInfo);
        var expectedValues = new Dictionary<int, List<int>>
        {
            {0, [5, 4]},
            {1, [8, 5]},
        };
        for (var i = 0; i < albumsInfo.Count; i++)
        {
            Assert.Equal(expectedValues[i][0], albumsInfo[i].album.Id);
            Assert.Equal(expectedValues[i][1], albumsInfo[i].tracksCount);
        }
    }

    /// <summary>
    /// Проверка вывода топ 5 альбомов по продолжительности 
    /// </summary>
    [Fact]
    public void TopAlbums()
    {
        var topAlbums =
            (from Album in fixture.GetAlbums()
             join Track in fixture.GetTracks()
             on Album.Id equals Track.AlbumId into albumTracks
             let totalTracksTime = albumTracks.Sum(t => t.Time.TotalSeconds)
             orderby totalTracksTime descending
             select new
             {
                album = Album,
                totalTime = totalTracksTime
             })
             .Take(5)
             .ToList();
        Assert.NotNull(topAlbums);
        var expectedValues = new Dictionary<int, List<int>>
        {
            {0, [2, 1404]},
            {1, [8, 1222]},
            {2, [4, 1101]},
            {3, [9, 1007]},
            {4, [1, 979]}
        };
        for (var i = 0; i < topAlbums.Count; i++)
        {
            Assert.Equal(expectedValues[i][0], topAlbums[i].album.Id);
            Assert.Equal(expectedValues[i][1], topAlbums[i].totalTime);
        }
    }

    /// <summary>
    /// Проверка вывода артистов с максимальным количеством альбомов
    /// </summary>
    [Fact]
    public void MaxAlbumsActors()
    {
        var topActors =
            (from Album in fixture.GetAlbums()
             group Album by Album.ActorId into albumGroup
             let albumCount = albumGroup.Count()
             let maxAlbumCount = 
                        (from al in fixture.GetAlbums()
                         group al by al.ActorId into alGroup
                         select alGroup.Count()).Max()
             where albumCount == maxAlbumCount
             join Actor in fixture.GetActors()
             on albumGroup.Key equals Actor.Id
             select new
             {
                 actor = Actor,
                 albumsCount = albumCount,
             })
            .ToList();
        Assert.NotNull(topActors);
        var expectedValues = new List<string> { "Ed Sheeran", "The Weeknd", "Drake", "Coldplay" };
        foreach (var actor in topActors)
        {
            Assert.Contains(actor.actor.Name, expectedValues);
        }
    }

    /// <summary>
    /// Проверка вывода информации о минимальной, средней и максимальной
    /// продолжительности альбомов
    /// </summary>
    [Fact]
    public void TimeAlbumInfo()
    {
        var albumsDurations =
            (from track in fixture.GetTracks()
            group track by track.AlbumId into trackGroup
            select trackGroup.Sum(t => t.Time.TotalSeconds))
            .ToList();
        Assert.NotNull(albumsDurations); 
        var minTime = albumsDurations.Min();
        var maxTime = albumsDurations.Max();
        var averageTime = albumsDurations.Average();
        Assert.Equal(1404, maxTime);
        Assert.Equal(709, minTime);
        Assert.Equal(1022.6, averageTime, 1);
    }
}