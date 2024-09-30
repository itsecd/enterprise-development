using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс льгота, предоставленная сотруднику.
/// </summary>
public class Benefit
{
    /// <summary>
    /// Уникальный идентификатор льготы
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Тип льготы 
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Сотрудник, которому предоставлена льгота
    /// </summary>
    public Employee Employee { get; set; }

    /// <summary>
    /// Дата предоставления льготы
    /// </summary>
    public DateTime Date { get; set; }
}