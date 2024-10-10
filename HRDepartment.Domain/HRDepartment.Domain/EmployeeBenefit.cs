using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain
{
    /// <summary>
    /// Промежуточный класс, представляющий льготы, которые получает сотрудник
    /// </summary>
    public class EmployeeBenefit
    {
        /// <summary>
        /// Уникальный идентификатор записи о льготе сотрудника
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, который получил льготу
        /// </summary>
        public required int EmployeeId { get; set; }

        /// <summary>
        /// Объект сотрудника, который получил льготу
        /// </summary>
        public required Employee Employee { get; set; }

        /// <summary>
        /// Идентификатор типа льготы
        /// </summary>
        public required int BenefitTypeId { get; set; }

        /// <summary>
        /// Объект типа льготы, которую получил сотрудник
        /// </summary>
        public required BenefitType BenefitType { get; set; }

        /// <summary>
        /// Дата выдачи льготы
        /// </summary>
        public required DateTime IssueDate { get; set; }
    }
}