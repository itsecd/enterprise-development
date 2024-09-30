using MediaLibrary.Domain;

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
            from actor in fixture.Actors
            orderby actor
            select actor;
        Assert.NotNull(actorInfo);
        Assert.Equal(9, actorInfo.Count());
    }

    /// <summary>
    /// Проверка вывода всех треков в альбоме, упорядоченных по номеру
    /// </summary>
    [Fact]
    public void TracksInAlbum()
    {
        var albumId = 1;
        var tracksInfo =
            (from album in fixture.Albums
            join track in fixture.Tracks on album.Id equals track.AlbumId
            where album.Id == albumId
            orderby track.Number
            select track).ToList();
        var expectedValues = new List<Track>()
        {
            fixture.Tracks[0],
            fixture.Tracks[1],
            fixture.Tracks[2],  
            fixture.Tracks[3],
        };
        Assert.NotNull(tracksInfo);
        Assert.Equal(expectedValues, tracksInfo);
    }

    /// <summary>
    /// Проверка вывода всех альбомов в указанный год с количеством треков в альбоме
    /// </summary>
    [Fact]
    public void AlbumsInfo()
    {
        var albumYear = 2022;
        var albumsInfo =
            (from album in fixture.Albums
             where album.Date.Year == albumYear
             join track in fixture.Tracks
             on album.Id equals track.AlbumId into albumTracks
             select new
             {
                 Album = album,
                 TracksCount = albumTracks.Count(),
             })
             .ToList();
        Assert.NotNull(albumsInfo);
        var expectedValues = new []
        {
            new  
            {
                Album = fixture.Albums[4],
                TrackCount = 4
            },
            new
            {
                Album = fixture.Albums[7],
                TrackCount = 5
            }
        };
        for (var i = 0; i < albumsInfo.Count; i++)
        {
            Assert.Equal(expectedValues[i].Album, albumsInfo[i].Album);
            Assert.Equal(expectedValues[i].TrackCount, albumsInfo[i].TracksCount);
        }
    }

    /// <summary>
    /// Проверка вывода топ 5 альбомов по продолжительности 
    /// </summary>
    [Fact]
    public void TopAlbums()
    {
        var topAlbums =
            (from album in fixture.Albums
             join track in fixture.Tracks
             on album.Id equals track.AlbumId into albumTracks
             group albumTracks by album into groupAlbums
             select new
             {
                 Album = groupAlbums.Key,
                 TotalTime = groupAlbums.Sum(g => g.Sum(t => t.Time.TotalSeconds))
             })
             .OrderByDescending(t => t.TotalTime)
             .Take(5)
             .ToList();
        Assert.NotNull(topAlbums);
        var expectedValues = new []
        {
            new
            {
                Album = fixture.Albums[1],
                TotalTime = 1404
            },
            new
            {
                Album = fixture.Albums[7],
                TotalTime = 1222
            },
            new
            {
                Album = fixture.Albums[3],
                TotalTime = 1101
            },
            new
            {
                Album = fixture.Albums[8],
                TotalTime = 1007
            },
            new
            {
                Album = fixture.Albums[0],
                TotalTime = 979
            },
        };
        for (var i = 0; i < topAlbums.Count; i++)
        {
            Assert.Equal(expectedValues[i].Album, topAlbums[i].Album);
            Assert.Equal(expectedValues[i].TotalTime, topAlbums[i].TotalTime);
        }
    }

    /// <summary>
    /// Проверка вывода артистов с максимальным количеством альбомов
    /// </summary>
    [Fact]
    public void MaxAlbumsActors()
    {
        var topActors =
            (from album in fixture.Albums
             group album by album.ActorId into albumGroup
             let albumCount = albumGroup.Count()
             let maxAlbumCount =
                        (from al in fixture.Albums
                         group al by al.ActorId into alGroup
                         select alGroup.Count()).Max()
             where albumCount == maxAlbumCount
             join actor in fixture.Actors
             on albumGroup.Key equals actor.Id
             select new
             {
                 Actor = actor,
                 AlbumsCount = albumCount,
             })
            .ToList();
        Assert.NotNull(topActors);
        var expectedValues = new[]
        {
            new 
            {
               Actor = fixture.Actors[1],
               AlbumsCount = 3
            },
            new
            {
               Actor = fixture.Actors[3],
               AlbumsCount = 3
            },
            new
            {
               Actor = fixture.Actors[5],
               AlbumsCount = 3
            },
            new
            {
               Actor = fixture.Actors[7],
               AlbumsCount = 3
            },
        };
        for (var i = 0; i < topActors.Count; i++)
        {
            Assert.Equal(expectedValues[i].Actor, topActors[i].Actor);
            Assert.Equal(expectedValues[i].AlbumsCount, topActors[i].AlbumsCount);
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
            (from track in fixture.Tracks
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