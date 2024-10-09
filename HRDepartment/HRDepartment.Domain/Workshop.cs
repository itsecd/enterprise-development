using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс, представляющий цех на предприятии
/// </summary>
public class Workshop
{
    /// <summary>
    /// Уникальный идентификатор цеха
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название цеха.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Коллекция сотрудников, работающих в данном цехе (один-ко-многим с Employee)
    /// </summary>
    public List<Employee> Employees { get; set; } = new List<Employee>();
}