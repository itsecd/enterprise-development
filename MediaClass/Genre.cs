namespace MediaClass
{
    /// <summary>
    /// class Genre
    /// 
    /// fields:
    /// - id : identification (primary key) of genre
    /// - name
    /// </summary>
    public class Genre
    {
        public int? _id { get; set; }
        private string? _name { get; set; }
    }
}