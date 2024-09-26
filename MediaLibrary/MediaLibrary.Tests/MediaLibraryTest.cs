namespace MediaLibrary.Tests;

public class MediaLibraryTest(MediaLibraryFixture fixture) : IClassFixture<MediaLibraryFixture>
{
    [Fact]
    public void Test1()
    {
        var a = fixture.GetGenres();
    }
}