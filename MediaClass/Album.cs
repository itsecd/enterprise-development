namespace MediaClass {
    /// <summary>
    /// class Album
    /// 
    /// fields:
    /// - id : identification (primary key) of album
    /// - title
    /// - release: date of release
    /// </summary>
    public class Album {
        public int ? Id {
            get;
            set;
        }
        public string ? Title {
            get;
            set;
        }
        public DateTime ? Release {
            get;
            set;
        }
    }
}