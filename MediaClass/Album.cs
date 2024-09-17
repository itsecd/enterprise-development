namespace MediaClass
{
    /// <summary>
    /// class Album
    /// 
    /// fields:
    /// - id : identification (primary key) of album
    /// - title
    /// - release: date of release
    /// </summary>
    public class Album
    {
        public int? _id { get; set; }
        public string? _title { get; set; }
        public DateTime? _release { get; set; }
    }
}