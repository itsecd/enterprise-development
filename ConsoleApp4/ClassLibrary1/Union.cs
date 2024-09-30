using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс профсоюзной организации
/// </summary>
public class Union
{
    /// <summary>
    /// Уникальный идентификатор профсоюзной организации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Список сотрудников, являющихся членами профсоюзной организации
    /// </summary>
    public List<Employee> Employees { get; set; } = new List<Employee>();

    /// <summary>
    /// Список льгот, предоставляемых профсоюзной организацией
    /// </summary>
    public List<Benefit> Benefits { get; set; } = new List<Benefit>();
}