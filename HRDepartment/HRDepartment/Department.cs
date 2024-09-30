using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс отдел, в котором работают сотрудники
/// </summary>
public class Department
{
    /// <summary>
    /// Уникальный идентификатор отдела
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название отдела
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Список сотрудников, работающих в отделе
    /// </summary>
    public List<Employee> Employees { get; set; }
}