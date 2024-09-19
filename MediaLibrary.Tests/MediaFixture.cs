namespace MediaLibrary.Tests;

/// <summary>
/// Заполнение тестовых списков артистов, альбомов, треков, жанров и связей жанр-артист
/// </summary>
public class MediaFixture
{
    /// <summary>
    /// Объявление сущности с тестовыми списками артистов, альбомов, треков, жанров и связей жанр-артист 
    /// </summary>
    public TestData TestData { get; set; }

    /// <summary>
    /// Заполнение тестовых списков исходными данными
    /// </summary>
    public MediaFixture()
    {
        TestData = new TestData
        {
            Artists = new()
            {
                new () { Id = 1, Name = "Imagine Dragons", Description = "American pop-rock band formed in 2008 in Las Vegas, USA" },
                new () { Id = 2, Name = "Lana Del Rey", Description = "American poet, singer-songwriter" },
                new () { Id = 3, Name = "Bonapart", Description = "Kazakh musician, rapper and producer" },
                new () { Id = 4, Name = "Масло черного тмина", Description = "Kazakhstani underground hip-hop artist" },
                new () { Id = 5, Name = "The Weekend", Description = "Canadian singer, songwriter and actor" }
            },
            Albums = new()
            {
                new () { Id = 1, Title = "After Hours", Release = new DateTime(2020, 3, 20), IdArtist = 5 },
                new () { Id = 2, Title = "Starboy", Release = new DateTime(2016, 11, 25), IdArtist = 5 },
                new () { Id = 3, Title = "Born to die", Release = new DateTime(2012, 12, 27), IdArtist = 2 },
                new () { Id = 4, Title = "Lie", Release = new DateTime(2020, 1, 12), IdArtist = 3 },
                new () { Id = 5, Title = "Origins", Release = new DateTime(2018, 11, 9), IdArtist = 4 },
                new () { Id = 6, Title = "Summer", Release = new DateTime(2018, 8, 23), IdArtist = 2 },
                new () { Id = 7, Title = "Radioactive", Release = new DateTime(2014, 4, 1), IdArtist = 1 }
            },
            Tracks = new()
            {
                new () { Id = 1, TrackNum = 3, Title = "Natural", IdAlbum = 5, Duration = TimeSpan.FromSeconds(122) },
                new () { Id = 2, TrackNum = 1, Title = "LIII", IdAlbum = 4, Duration = TimeSpan.FromSeconds(236) },
                new () { Id = 3, TrackNum = 5, Title = "Born", IdAlbum = 4, Duration = TimeSpan.FromSeconds(651) },
                new () { Id = 4, TrackNum = 1, Title = "Party Monster", IdAlbum = 2, Duration = TimeSpan.FromSeconds(111) },
                new () { Id = 5, TrackNum = 3, Title = "Alone Again", IdAlbum = 6, Duration = TimeSpan.FromSeconds(541) },
                new () { Id = 6, TrackNum = 7, Title = "Feel like", IdAlbum = 2, Duration = TimeSpan.FromSeconds(298) },
                new () { Id = 7, TrackNum = 2, Title = "Back please", IdAlbum = 1, Duration = TimeSpan.FromSeconds(300) }
            },
            Genres = new()
            {
                new () { Id = 1, Name = "Rock" },
                new () { Id = 2, Name = "Pop" },
                new () { Id = 3, Name = "Hip-hop" }
            },
            ParticipationArtistGenre = new()
            {
                new () { IdGenre = 1, IdArtist = 1 },
                new () { IdGenre = 1, IdArtist = 2 },
                new () { IdGenre = 2, IdArtist = 3 },
                new () { IdGenre = 2, IdArtist = 4 },
                new () { IdGenre = 3, IdArtist = 4 },
                new () { IdGenre = 2, IdArtist = 5 },
                new () { IdGenre = 3, IdArtist = 2 },
                new () { IdGenre = 1, IdArtist = 3 },
                new () { IdGenre = 3, IdArtist = 3 }

            }
        };
    }
}