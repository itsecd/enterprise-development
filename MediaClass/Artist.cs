namespace MediaClass {
    /// <summary>
    /// class Artist
    /// 
    /// fields:
    /// - id : identification (primary key) of artist
    /// - name
    /// - description
    /// </summary>
    public class Artist {
        public int ? Id {
            get;
            set;
        }
        public string ? Name {
            get;
            set;
        }
        public string ? Description {
            get;
            set;
        }
    }
}