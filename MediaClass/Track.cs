namespace MediaClass {
    /// <summary>
    /// class Track
    /// 
    /// fields:
    /// - id : identification (primary key) of track
    /// - trackNum: number of track
    /// - title
    /// - duration
    /// - idAlbum: identification of album
    /// </summary>
    public class Track {
        public int ? Id {
            get;
            set;
        }
        public int ? TrackNum {
            get;
            set;
        }
        public string ? Title {
            get;
            set;
        }
        public int ? Duration {
            get;
            set;
        }
        public int ? IdAlbum {
            get;
            set;
        }
    }
}