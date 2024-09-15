using Query.Impl.Models.BaseModel;

namespace Query.Impl.Models
{
    /// <summary>
    /// Реализация сущности факультет
    /// </summary>
    public class Faculty: EntityWithName
    {
        #region ctors
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="institution"></param>
        /// <param name="departments"></param>
        public Faculty(string name, Institution institution, ICollection<Department> departments) : base(name) 
        {
            Institution = institution;
            Departments = departments;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public Faculty() { }
        #endregion

        #region Fields
        /// <summary>
        /// 
        /// </summary>
        public virtual Institution Institution { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Department> Departments { get; set; }
        #endregion

    }
}
