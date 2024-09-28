using MediaLibrary.Domain;

namespace MediaLibrary.Tests;

/// <summary>
/// Класс для тестовых данных
/// </summary>
public class MediaLibraryFixture
{
    /// <summary>
    /// Список исполнителей 
    /// </summary>
    private readonly List<Actor>? _actors;

    /// <summary>
    /// Список сущностей Исполнитель-Жанр
    /// </summary>
    private readonly List<ActorGenre>? _actorGenres;

    /// <summary>
    /// Список жанров
    /// </summary>
    private readonly List<Genre>? _genres;

    /// <summary>
    /// Список треков
    /// </summary>
    private readonly List<Track>? _tracks;

    /// <summary>
    /// Список альбомов
    /// </summary>
    private readonly List<Album>? _albums;

    /// <summary>
    /// Инициализация тестовых данных 
    /// </summary>
    public MediaLibraryFixture()
    {
        _actors = new() 
        {
            new() { Id = 1, Name = "Beyoncé", 
                Description = "American singer, songwriter, and actress, one of the best-selling music artists of all time" },
            new() { Id = 2, Name = "Ed Sheeran",
                Description = "English singer-songwriter known for his hits 'Shape of You' and 'Perfect'" },
            new() { Id = 3, Name = "Taylor Swift", 
                Description = "American singer-songwriter, known for blending country, pop, and alternative music" },
            new() { Id = 4, Name = "The Weeknd", 
                Description = "Canadian singer and record producer, recognized for his dark, moody R&B style" },
            new() { Id = 5, Name = "Billie Eilish", 
                Description = "American singer-songwriter known for her unique, whispery vocal style and hits like 'Bad Guy'" },
            new() { Id = 6, Name = "Drake", 
                Description = "Canadian rapper, singer, and actor, one of the most influential figures in modern rap" },
            new() { Id = 7, Name = "Adele", 
                Description = "British singer-songwriter, famous for powerful ballads like 'Hello' and 'Someone Like You'" },
            new() { Id = 8, Name = "Coldplay", 
                Description = "British rock band formed in 1996, known for their anthemic sound and hit albums like 'A Rush of Blood to the Head'" },
            new() { Id = 9, Name = "Dua Lipa", 
                Description = "British-Albanian singer-songwriter, known for her pop hits 'Don't Start Now' and 'Levitating'" }
        };

        _genres = new()
        {
            new() { Id = 1, Name = "Pop" },
            new() { Id = 2, Name = "R&B" },
            new() { Id = 3, Name = "Alternative Pop" },
            new() { Id = 4, Name = "Hip-Hop" },
            new() { Id = 5, Name = "Rock" }
        };

        _actorGenres = new()
        {
            new() { ActorId = 1, GenreId = 1 }, 
            new() { ActorId = 2, GenreId = 1 }, 
            new() { ActorId = 3, GenreId = 1 }, 
            new() { ActorId = 4, GenreId = 2 }, 
            new() { ActorId = 5, GenreId = 3 }, 
            new() { ActorId = 6, GenreId = 4 }, 
            new() { ActorId = 7, GenreId = 1 }, 
            new() { ActorId = 8, GenreId = 5 }, 
            new() { ActorId = 9, GenreId = 1 }  
        };

        _albums = new()
        {
            new() { Id = 1, Name= "Lemonade", 
                Date= new DateTime(2016, 4, 23), ActorId = 1 }, 
            new() { Id = 2, Name= "Divide (÷)", 
                Date= new DateTime(2017, 3, 3), ActorId = 2 }, 
            new() { Id = 3, Name= "Multiply (×)",
                Date= new DateTime(2014, 6, 20), ActorId = 2 }, 
            new() { Id = 4, Name= "Equals (=)", 
                Date= new DateTime(2021, 10, 29), ActorId = 2 }, 
            new() { Id = 5, Name= "Midnights", 
                Date= new DateTime(2022, 10, 21), ActorId = 3 }, 
            new() { Id = 6, Name= "After Hours", 
                Date= new DateTime(2020, 3, 20), ActorId = 4 }, 
            new() { Id = 7, Name= "Starboy", 
                Date= new DateTime(2016, 11, 25),  ActorId = 4 },
            new() { Id = 8, Name= "Dawn FM", 
                Date= new DateTime(2022, 1, 7), ActorId = 4 }, 
            new() { Id = 9, Name= "Happier Than Ever", 
                Date= new DateTime(2021, 7, 30), ActorId = 5 }, 
            new() { Id = 10, Name= "Scorpion", 
                Date= new DateTime(2018, 6, 29), ActorId = 6 },
            new() { Id = 11, Name= "Views", 
                Date= new DateTime(2016, 4, 29), ActorId = 6 }, 
            new() { Id = 12, Name= "Nothing Was the Same", 
                Date= new DateTime(2013, 9, 24), ActorId = 6 }, 
            new() { Id = 13, Name= "25", 
                Date= new DateTime(2015, 11, 20), ActorId = 7 },
            new() { Id = 14, Name= "A Head Full of Dreams", 
                Date= new DateTime(2015, 12, 4), ActorId = 8 }, 
            new() { Id = 15, Name= "Mylo Xyloto", 
                Date= new DateTime(2011, 10, 24), ActorId = 8 }, 
            new() { Id = 16, Name= "Ghost Stories", 
                Date= new DateTime(2014, 5, 16), ActorId = 8 }, 
            new() { Id = 17, Name= "Future Nostalgia", 
                Date= new DateTime(2020, 3, 27), ActorId = 9 }  
        };

        _tracks = new()
        {
            new() { Id = 1, Number = 1, Name = "Pray You Catch Me", 
                AlbumId = 1, Time = TimeSpan.FromSeconds(229) },
            new() { Id = 2, Number = 2, Name = "Hold Up", 
                AlbumId = 1, Time = TimeSpan.FromSeconds(261) },
            new() { Id = 3, Number = 3, Name = "Sorry", 
                AlbumId = 1, Time = TimeSpan.FromSeconds(224) },
            new() { Id = 4, Number = 4, Name = "Freedom", 
                AlbumId = 1, Time = TimeSpan.FromSeconds(265) },

            new() { Id = 5, Number = 1, Name = "Eraser", 
                AlbumId = 2, Time = TimeSpan.FromSeconds(228) },
            new() { Id = 6, Number = 2, Name = "Castle on the Hill", 
                AlbumId = 2, Time = TimeSpan.FromSeconds(261) },
            new() { Id = 7, Number = 3, Name = "Dive", 
                AlbumId = 2, Time = TimeSpan.FromSeconds(197) },
            new() { Id = 8, Number = 4, Name = "Happier", 
                AlbumId = 2, Time = TimeSpan.FromSeconds(212) },
            new() { Id = 9, Number = 5, Name = "New Man", 
                AlbumId = 2, Time = TimeSpan.FromSeconds(243) },
            new() { Id = 10, Number = 6, Name = "Hearts Don't Break Around Here", 
                AlbumId = 2, Time = TimeSpan.FromSeconds(263) },

            new() { Id = 11, Number = 1, Name = "Willow", 
                AlbumId = 3, Time = TimeSpan.FromSeconds(211) },
            new() { Id = 12, Number = 2, Name = "Champagne Problems", 
                AlbumId = 3, Time = TimeSpan.FromSeconds(254) },
            new() { Id = 13, Number = 3, Name = "Gold Rush",
                AlbumId = 3, Time = TimeSpan.FromSeconds(208) },
            new() { Id = 14, Number = 4, Name = "Tis the Damn Season", 
                AlbumId = 3, Time = TimeSpan.FromSeconds(241) },

            new() { Id = 15, Number = 1, Name = "Alone Again", 
                AlbumId = 4, Time = TimeSpan.FromSeconds(234) },
            new() { Id = 16, Number = 2, Name = "Too Late", 
                AlbumId = 4, Time = TimeSpan.FromSeconds(198) },
            new() { Id = 17, Number = 3, Name = "Hardest to Love", 
                AlbumId = 4, Time = TimeSpan.FromSeconds(237) },
            new() { Id = 18, Number = 4, Name = "Scared to Live",
                AlbumId = 4, Time = TimeSpan.FromSeconds(200) },
            new() { Id = 19, Number = 5, Name = "Snowchild", 
                AlbumId = 4, Time = TimeSpan.FromSeconds(232) },

            new() { Id = 20, Number = 1, Name = "Getting Older", 
                AlbumId = 5, Time = TimeSpan.FromSeconds(242) },
            new() { Id = 21, Number = 2, Name = "I Didn’t Change My Number", 
                AlbumId = 5, Time = TimeSpan.FromSeconds(237) },
            new() { Id = 22, Number = 3, Name = "Billie Bossa Nova", 
                AlbumId = 5, Time = TimeSpan.FromSeconds(218) },
            new() { Id = 23, Number = 4, Name = "Lost Cause", 
                AlbumId = 5, Time = TimeSpan.FromSeconds(240) },

            new() { Id = 24, Number = 1, Name = "Survival", 
                AlbumId = 6, Time = TimeSpan.FromSeconds(200) },
            new() { Id = 25, Number = 2, Name = "Elevate", 
                AlbumId = 6, Time = TimeSpan.FromSeconds(265) },
            new() { Id = 26, Number = 3, Name = "In My Feelings", 
                AlbumId = 6, Time = TimeSpan.FromSeconds(244) },

            new() { Id = 27, Number = 1, Name = "Hello", 
                AlbumId = 7, Time = TimeSpan.FromSeconds(245) },
            new() { Id = 28, Number = 2, Name = "Send My Love", 
                AlbumId = 7, Time = TimeSpan.FromSeconds(222) },
            new() { Id = 29, Number = 3, Name = "I Miss You", 
                AlbumId = 7, Time = TimeSpan.FromSeconds(248) },
            new() { Id = 30, Number = 4, Name = "Water Under the Bridge", 
                AlbumId = 7, Time = TimeSpan.FromSeconds(215) },

            new() { Id = 31, Number = 1, Name = "A Head Full of Dreams", 
                AlbumId = 8, Time = TimeSpan.FromSeconds(271) },
            new() { Id = 32, Number = 2, Name = "Hymn for the Weekend", 
                AlbumId = 8, Time = TimeSpan.FromSeconds(230) },
            new() { Id = 33, Number = 3, Name = "Adventure of a Lifetime", 
                AlbumId = 8, Time = TimeSpan.FromSeconds(235) },
            new() { Id = 34, Number = 4, Name = "Everglow", 
                AlbumId = 8, Time = TimeSpan.FromSeconds(232) },
            new() { Id = 35, Number = 5, Name = "Up&Up", 
                AlbumId = 8, Time = TimeSpan.FromSeconds(254) },

            new() { Id = 36, Number = 1, Name = "Don't Start Now",
                AlbumId = 9, Time = TimeSpan.FromSeconds(183) },
            new() { Id = 37, Number = 2, Name = "Physical", 
                AlbumId = 9, Time = TimeSpan.FromSeconds(183) },
            new() { Id = 38, Number = 3, Name = "Levitating", 
                AlbumId = 9, Time = TimeSpan.FromSeconds(203) },
            new() { Id = 39, Number = 4, Name = "Pretty Please", 
                AlbumId = 9, Time = TimeSpan.FromSeconds(209) },
            new() { Id = 40, Number = 5, Name = "Love Again", 
                AlbumId = 9, Time = TimeSpan.FromSeconds(229) },
        };
    }

    /// <summary>
    /// Получить список альбомов
    /// </summary>
    /// <returns>Список альбомов</returns>
    public List<Album> GetAlbums() { return _albums!; }
    
    /// <summary>
    /// Получить список артистов
    /// </summary>
    /// <returns>Список артистов</returns>
    public List<Actor> GetActors() { return _actors!; }

    /// <summary>
    /// Получить список жанров 
    /// </summary>
    /// <returns>Список жанров</returns>
    public List<Genre> GetGenres() { return _genres!; }

    /// <summary>
    /// Получить список треков
    /// </summary>
    /// <returns>Список треков</returns>
    public List<Track> GetTracks() { return _tracks!; }

    /// <summary>
    /// Получить список Исполнитель-Жанр
    /// </summary>
    /// <returns>Список Исполнитель-Жанр</returns>
    public List<ActorGenre> GetActorGenres() { return _actorGenres!; }
}
