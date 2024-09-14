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
        /// <param name="university"></param>
        /// <param name="departments"></param>
        public Faculty(string name, University university, ICollection<Department> departments) : base(name) 
        {
            University = university;
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
        public virtual University University { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Department> Departments { get; set; }
        #endregion

    }
}
