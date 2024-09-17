using MediaClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Tests {
    /// <summary>
    /// class TestData
    /// contains lists of class instances of Artists, Albums, Tracks, Genres
    /// and its getters and setters
    /// </summary>
    public class TestData {
        public List < Artist > ? Artists {
            get;
            set;
        }
        public List < Album > ? Albums {
            get;
            set;
        }
        public List < Track > ? Tracks {
            get;
            set;
        }
        public List < Genre > ? Genres {
            get;
            set;
        }
    }
}