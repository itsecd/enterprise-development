namespace MediaLibrary.Tests {
    /// <summary> 
    /// class MediaLibraryTests
    /// contains unit tests for verifying the correct retrieval of data from a media library. 
    /// Utilizes the <see cref="MediaFixture"/> class to provide test data for artists, albums, tracks, and genres. 
    /// </summary>
    public class MediaLibraryTests: IClassFixture < MediaFixture > {
        private readonly MediaFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaLibraryTests"/> 
        /// class with the specified <see cref="MediaFixture"/> instance.
        /// </summary>
        public MediaLibraryTests(MediaFixture fixture) {
            _fixture = fixture;
        }

        /// <summary>
        /// Tests the retrieval of an artist from the test data by verifying the artist's details.
        /// </summary>
        [Fact]
        public void TestArtistRetrieval() {
            var artist = _fixture.TestData.Artists.FirstOrDefault(a => a.Id == 1);
            Assert.NotNull(artist);
            Assert.Equal("Imagine Dragons", artist.Name);
            Assert.Equal("American pop-rock band formed in 2008 in Las Vegas, USA", artist.Description);
        }

        /// <summary>
        /// Tests the retrieval of an album from the test data by verifying the album's details.
        /// </summary>
        [Fact]
        public void TestAlbumRetrieval() {
            var album = _fixture.TestData.Albums.FirstOrDefault(a => a.Id == 2);
            Assert.NotNull(album);
            Assert.Equal("Starboy", album.Title);
            Assert.Equal(new DateTime(2016, 11, 25), album.Release);
        }

        /// <summary>
        /// Tests the retrieval of a track from the test data by verifying the track's details.
        /// </summary>
        [Fact]
        public void TestTrackRetrieval() {
            var track = _fixture.TestData.Tracks.FirstOrDefault(t => t.IdAlbum == 4);
            Assert.NotNull(track);
            Assert.Equal("LIII", track.Title);
            Assert.Equal(236, track.Duration);
        }

        /// <summary>
        /// Tests the retrieval of a genre from the test data by verifying the genre's details.
        /// </summary>
        [Fact]
        public void TestGenreRetrieval() {
            var genre = _fixture.TestData.Genres.FirstOrDefault(g => g.Id == 3);
            Assert.NotNull(genre);
            Assert.Equal("Hip-hop", genre.Name);
        }
    }
}