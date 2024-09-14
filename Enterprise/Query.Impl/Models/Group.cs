using Query.Impl.Models.BaseModel;

namespace Query.Impl.Models
{
    /// <summary>
    /// Реализация сущности группа
    /// </summary>
    public class Group: Entity
    {
        #region ctors
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="number"></param>
        /// <param name="department"></param>
        /// <param name="speciality"></param>
        public Group(string number, Department department, Speciality speciality ) 
        {
            Number = number;
            Department = department;
            Speciality = speciality;
        }
        
        /// <summary>
        /// ctor
        /// </summary>
        public Group() { }

        #endregion

        #region Field
        /// <summary>
        /// 
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Speciality Speciality { get; set; }
        #endregion

    }
}
