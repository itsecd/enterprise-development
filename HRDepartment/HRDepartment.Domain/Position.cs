using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс, представляющий должность на предприятии
/// </summary>
public class Position
{
    /// <summary>
    /// Уникальный идентификатор должности
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название должности
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Идентификатор отдела, к которому относится должность
    /// </summary>
    public required int DepartmentId { get; set; }

    /// <summary>
    /// Объект отдела, к которому относится должность (связь с Department)
    /// </summary>
    public required Department Department { get; set; }

    /// <summary>
    /// Коллекция сотрудников, занимающих эту должность 
    /// </summary>
    public List<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
}