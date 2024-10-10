using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain
{
    /// <summary>
    /// Класс, представляющий тип льготы
    /// </summary>
    public class BenefitType
    {
        /// <summary>
        /// Уникальный идентификатор типа льготы
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Название типа льготы
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Коллекция льгот, которые были выданы сотрудникам (многие-ко-многим с Employee через EmployeeBenefit)
        /// </summary>
        public List<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();
    }
}