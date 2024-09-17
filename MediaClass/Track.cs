namespace MediaClass
{
    /// <summary>
    /// class Track
    /// 
    /// fields:
    /// - id : identification (primary key) of track
    /// - trackNum: number of track
    /// - title
    /// - duration
    /// - idAlbum: external key of album
    /// </summary>
    public class Track
    {
        public int? _id { get; set; }
        public int? _trackNum { get; set; }
        public string? _title { get; set; }
        public int? _duration { get; set; }
        public int? _idAlbum { get; set; }
    }
}