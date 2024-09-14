namespace Query.Impl.Models.BaseModel
{
    /// <summary>
    /// Базовый класс для сущности с именем
    /// </summary>
    public abstract class EntityWithName
    {
        #region ctors
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name"></param>
        public EntityWithName(string name) 
        {
            Name = name;
            Id = Guid.NewGuid();
            Version = DateTime.Now;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public EntityWithName()
        { 
            Id = Guid.NewGuid();
            Version = DateTime.Now;
        }
        #endregion

        #region Fields
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Version { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}
