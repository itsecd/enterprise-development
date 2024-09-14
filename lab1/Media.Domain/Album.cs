using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    internal class Album
    {
        public string Name { get; set; }
        public int ReleaseDate {  get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
        public Artist Artist { get; set; }

    }
}
