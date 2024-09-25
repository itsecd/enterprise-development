﻿using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация сущности факультет
/// </summary>
public class Faculty: EntityWithName
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="institution"></param>
    /// <param name="departments"></param>
    public Faculty(string name, Institution institution, ICollection<Department> departments) : base(name) 
    {
        Institution = institution;
        Departments = departments;
    }


    /// <summary>
    /// Институт
    /// </summary>
    required public virtual Institution Institution { get; set; }

    /// <summary>
    /// Кафедры
    /// </summary>
    required public virtual ICollection<Department> Departments { get; set; }

}
