namespace Query.Impl.Models.BaseModel
{
    /// <summary>
    /// Базовый класс для сущности
    /// </summary>
    public abstract class Entity
    {
        #region ctors
        /// <summary>
        /// ctor
        /// </summary>
        protected Entity() 
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
        #endregion
    }
}
