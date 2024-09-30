using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс должность в компании
/// </summary>
public class Position
{
    /// <summary>
    /// Уникальный идентификатор должности
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название должности
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список сотрудников, занимающих эту должность
    /// </summary>
    public List<Employee> Employees { get; set; } = new List<Employee>();
}