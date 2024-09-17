using MediaClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MediaLibrary.Tests {
    /// <summary>
    /// class MediaFixture
    /// provides a fixture for testing purposes by setting up a collection of test data
    /// related to a media library. This includes lists of instances for artists, albums,
    /// tracks, and genres, which are used in unit tests.
    /// </summary>
    public class MediaFixture: IDisposable {
        public TestData TestData {
            get;
            set;
        }

        public MediaFixture() {
            TestData = new TestData {
                Artists = new List < Artist > {
                        new Artist {
                            Id = 1, Name = "Imagine Dragons", Description =
                                "American pop-rock band formed in 2008 in Las Vegas, USA"
                        },
                        new Artist {
                            Id = 2, Name = "Lana Del Rey", Description = "American poet, singer-songwriter"
                        },
                        new Artist {
                            Id = 3, Name = "Bonapart", Description = "Kazakh musician, rapper and producer"
                        },
                        new Artist {
                            Id = 4, Name = "Масло черного тмина", Description = "Kazakhstani underground hip-hop artist"
                        },
                        new Artist {
                            Id = 5, Name = "The Weekend", Description = "Canadian singer, songwriter and actor"
                        }
                    },
                    Albums = new List < Album > {
                        new Album {
                            Id = 1, Title = "After Hours", Release = new DateTime(2020, 3, 20)
                        },
                        new Album {
                            Id = 2, Title = "Starboy", Release = new DateTime(2016, 11, 25)
                        },
                        new Album {
                            Id = 3, Title = "Born to die", Release = new DateTime(2012, 12, 27)
                        },
                        new Album {
                            Id = 4, Title = "Lie", Release = new DateTime(2024, 1, 12)
                        },
                        new Album {
                            Id = 5, Title = "Origins", Release = new DateTime(2018, 11, 9)
                        }
                    },
                    Tracks = new List < Track > {
                        new Track {
                            Id = 1, TrackNum = 24221, Title = "Natural", IdAlbum = 5, Duration = 122
                        },
                        new Track {
                            Id = 2, TrackNum = 45223, Title = "LIII", IdAlbum = 4, Duration = 236
                        },
                        new Track {
                            Id = 3, TrackNum = 23421, Title = "Born", IdAlbum = 3, Duration = 651
                        },
                        new Track {
                            Id = 4, TrackNum = 54654, Title = "Party Monster", IdAlbum = 2, Duration = 111
                        },
                        new Track {
                            Id = 5, TrackNum = 45163, Title = "Alone Again", IdAlbum = 1, Duration = 541
                        }
                    },
                    Genres = new List < Genre > {
                        new Genre {
                            Id = 1, Name = "Rock"
                        },
                        new Genre {
                            Id = 2, Name = "Pop"
                        },
                        new Genre {
                            Id = 3, Name = "Hip-hop"
                        }
                    }
            };
        }

        public void Dispose() {}
    }
}