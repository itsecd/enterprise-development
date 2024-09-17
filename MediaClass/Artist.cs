namespace MediaClass
{
    /// <summary>
    /// class Artist
    /// 
    /// fields:
    /// - id : identification (primary key) of artist
    /// - name
    /// - description
    /// </summary>
    public class Artist
    {
        public int? _id { get; set; }
        public string? _name { get; set; }
        public string? _description { get; set; }
    }
}