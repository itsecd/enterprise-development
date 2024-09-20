using MediaLibrary.Domain.Models;

namespace MediaLibrary.Tests.Fixtures;

public class MediaLibraryFixture
{
    public List<Artist> Artists = new()
    {
        new() { ArtistId = 1, Name = "Jon Bon Jovi",
                Description = "У него есть кожаная куртка с дюжиной застежек",
                AlbumIds = new List<int> { 1, 2 }, GenreIds = new List<int> { 1, 2 } },

        new() { ArtistId = 2, Name = "Bob Seger",
                Description = "Его не заткнуть, а его смех похож на скрежет бетономешалки",
                AlbumIds = new List<int> { 3, 4 }, GenreIds = new List<int> { 3 } },

        new() { ArtistId = 3, Name = "Jay Kay",
                Description = "Он как-то говорил, что не смог бы стать раллийным гонщиком",
                AlbumIds = new List<int> { 5 }, GenreIds = new List<int> { 4 } },

        new() { ArtistId = 4, Name = "Roger Daltrey",
                Description = "Самый быстрый человек с билетом на автобус",
                AlbumIds = new List<int> { 6, 7 }, GenreIds = new List<int> { 3 } },

        new() { ArtistId = 5, Name = "Lionel Richie",
                Description = "Он часто оказывался на обочине и пытался открыть правую дверь",
                AlbumIds = new List<int> { 8 }, GenreIds = new List<int> { 4 } },

        new() { ArtistId = 6, Name = "Cliff Richard",
                Description = "Его усилия разогнаться иссякли на 130 километрах в час",
                AlbumIds = new List<int> { 9, 10 }, GenreIds = new List<int> { 4, 5 } },

        new() { ArtistId = 7, Name = "James Blunt",
                Description = "Оденажды он спел о чем-то там в море",
                AlbumIds = new List<int> { 11, 12 }, GenreIds = new List<int> { 3, 5 } }
    };

    public List<Song> Songs = new()
    {
        new() { SongId = 1, AlbumName = "Stronger", Name = "Stronger Than That",
                Duration = TimeSpan.FromSeconds(281), NumberInAlbum = 1 },

        new() { SongId = 2, AlbumName = "Stronger", Name = "Who's In Love",
                Duration = TimeSpan.FromSeconds(270), NumberInAlbum = 2 },

        new() { SongId = 3, AlbumName = "Stronger", Name = "The Best Of Me",
                Duration = TimeSpan.FromSeconds(250), NumberInAlbum = 3 },

        new() { SongId = 4, AlbumName = "Stronger", Name = "Clear Blue Skies",
                Duration = TimeSpan.FromSeconds(174), NumberInAlbum = 4 },

        new() { SongId = 5, AlbumName = "Stronger", Name = "Lean On You",
                Duration = TimeSpan.FromSeconds(302), NumberInAlbum = 5 },

        new() { SongId = 6, AlbumName = "Stronger", Name = "Keep Me Warm",
                Duration = TimeSpan.FromSeconds(264), NumberInAlbum = 6 }
    };

    public List<Album> Albums = new()
    {
        new() { AlbumId = 1, Title = "Against The Wind", ArtistId = 2,
                ReleaseDate = 1980, SongIds = new List<int> { 1, 2, 12, 14 } },

        new() { AlbumId = 2, Title = "Stranger in Town", ArtistId = 2,
                ReleaseDate = 1978, SongIds = new List<int> { 4, 5, 24, 45, 444 } },

        new() { AlbumId = 3, Title = "Nine Tonight", ArtistId = 3,
                ReleaseDate = 1980, SongIds = new List<int> { 6, 7, 86, 90 } },

        new() { AlbumId = 4, Title = "Beautiful Loser", ArtistId = 2,
                ReleaseDate = 2075, SongIds = new List<int> { 9, 11, 100, 1120 } }
    };

    public List<Song> SongsInOneAlbum = new()
    {
        new() { SongId = 1, AlbumName = "Against The Wind", Name = "The Horisontal Bop",
                Duration = TimeSpan.FromSeconds(241), NumberInAlbum = 1 },

        new() { SongId = 2, AlbumName = "Against The Wind", Name = "No Man's Land",
                Duration = TimeSpan.FromSeconds(260), NumberInAlbum = 2 },

        new() { SongId = 3, AlbumName = "Against The Wind", Name = "Good For Me",
                Duration = TimeSpan.FromSeconds(199), NumberInAlbum = 3 },

        new() { SongId = 4, AlbumName = "Stranger in Town", Name = "Hollywood Nights",
                Duration = TimeSpan.FromSeconds(194), NumberInAlbum = 1 },

        new() { SongId = 5, AlbumName = "Stranger in Town", Name = "Till It Shines",
                Duration = TimeSpan.FromSeconds(302), NumberInAlbum = 2 },

        new() { SongId = 6, AlbumName = "Nine Tonight", Name = "Nine Tonight",
                Duration = TimeSpan.FromSeconds(264), NumberInAlbum = 1 },

        new() { SongId = 7, AlbumName = "Beautiful Loser", Name = "Beautiful Loser",
                Duration = TimeSpan.FromSeconds(305), NumberInAlbum = 1 },

        new() { SongId = 8, AlbumName = "Beautiful Loser", Name = "Jody Girl",
                Duration = TimeSpan.FromSeconds(184), NumberInAlbum = 2 },

        new() { SongId = 9, AlbumName = "Beautiful Loser", Name = "Black Night",
                Duration = TimeSpan.FromSeconds(234), NumberInAlbum = 3 }
    };

    public List<Song> SongsForTopFiveAlbums = new()
    {
        new() { SongId = 1, AlbumName = "What is the word?", Duration = TimeSpan.FromSeconds(100),
                Name = "A.. bird. Bird is the word", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "B B B B Bird", Duration = TimeSpan.FromSeconds(101),
                Name = "Bird, bird, bird, b-bird's the word", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "Bird Is The Word", Duration = TimeSpan.FromSeconds(102),
                Name = "A-well, a bird, bird, bird, bird is the word", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "Do You Or Do You Not Know", Duration = TimeSpan.FromSeconds(103),
                Name = "A-well, a bird, bird, bird, well-a bird is the word", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "About The Bird", Duration = TimeSpan.FromSeconds(104),
                Name = "A-well, a bird, bird, bird, b-bird's the word", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "Couse Everybodys Heard That The Bird Is The Word",
                Duration = TimeSpan.FromSeconds(105), Name = "A-well, a bird, bird, b-bird is the word", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "Oh Have You Not Heard?", Duration = TimeSpan.FromSeconds(106),
                Name = "A-well-a don't you know about the bird?", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "It Was My Understanding That Everyone Had Heard",
                Duration = TimeSpan.FromSeconds(107), Name = "A-well-a-bird, bird, b-bird's the word, a-well-a", NumberInAlbum = 1 },

        new() { SongId = 1, AlbumName = "A-well, A Bird, Bird, B-bird Is The Word",
                Duration = TimeSpan.FromSeconds(108), Name = "A-well-a bird, surfing bird", NumberInAlbum = 1 }
    };
}
