using Query.Impl.Models.BaseModel;

namespace Query.Impl.Models
{
    /// <summary>
    /// Реализация сущности специальность
    /// </summary>
    public class Speciality: EntityWithName
    {
        #region ctors
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="groups"></param>
        public Speciality(string name, string code, ICollection<Group> groups): base(name)
        {
            Code = code;
            Groups = groups;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public Speciality() { }
        #endregion

        #region Fields
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Institution Institution { get; set; }
        #endregion
    }
}
