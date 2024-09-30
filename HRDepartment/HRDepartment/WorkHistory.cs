using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс, который представляет запись о трудовой деятельности сотрудника
/// </summary>
public class WorkHistory
{
    /// <summary>
    /// Уникальный идентификатор записи о трудовой деятельности
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Сотрудник, для которого ведется трудовая деятельность
    /// </summary>
    public required Employee Employee { get; set; }

    /// <summary>
    /// Отдел, в котором работает сотрудник
    /// </summary>
    public required Department Department { get; set; }

    /// <summary>
    /// Должность, которую занимал сотрудник
    /// </summary>
    public required Position Position { get; set; }

    /// <summary>
    /// Дата начала трудовой деятельности
    /// </summary>
    public required DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания трудовой деятельности (если применимо)
    /// </summary>
    public required DateTime? EndDate { get; set; }
}