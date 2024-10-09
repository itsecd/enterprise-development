using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Промежуточный класс, представляющий связь между сотрудником и должностью
/// </summary>
public class EmployeePosition
{
    /// <summary>
    /// Уникальный идентификатор записи о позиции сотрудника
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор сотрудника, связанного с должностью
    /// </summary>
    public required int EmployeeId { get; set; }

    /// <summary>
    /// Объект сотрудника, связанного с этой должностью
    /// </summary>
    public required Employee Employee { get; set; }

    /// <summary>
    /// Идентификатор должности, на которой работает сотрудник
    /// </summary>
    public required int PositionId { get; set; }

    /// <summary>
    /// Объект должности, которую занимает сотрудник
    /// </summary>
    public required Position Position { get; set; }

    /// <summary>
    /// Дата принятия сотрудника на данную должность
    /// </summary>
    public required DateTime EmploymentDate { get; set; }

    /// <summary>
    /// Дата увольнения сотрудника с должности
    /// </summary>
    public required DateTime? RetirementDate { get; set; }
}