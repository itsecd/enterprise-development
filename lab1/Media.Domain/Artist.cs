using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    internal class Artist
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();
        public List<Genre> Genres { get; set; } = new List<Genre> { };
    }
}
