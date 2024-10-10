using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс, представляющий отдел на предприятии
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
    /// Коллекция должностей, принадлежащих данному отделу
    /// </summary>
    public List<Position> Positions { get; set; } = new List<Position>();
}