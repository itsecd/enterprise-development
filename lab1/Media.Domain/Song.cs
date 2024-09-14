using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Media.Domain
{
    public class Song
    {
        public string Name { get; set; }
        public int NumberInAlbum {  get; set; }
        public string AlbumName {  get; set; }
        public TimeSpan Duration {  get; set; }
    }
}