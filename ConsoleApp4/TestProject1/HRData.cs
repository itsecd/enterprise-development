using HRDepartment.Domain;

namespace HRDepartment.Tests;

/// <summary>
/// Класс, который представляет данные для тестирования 
/// </summary>
public class HRData
{
    /// <summary>
    /// Список сотрудников
    /// </summary>
    public List<Employee>? Employees { get; set; }

    /// <summary>
    /// Список отделов
    /// </summary>
    public List<Department>? Departments { get; set; }

    /// <summary>
    /// Список должностей
    /// </summary>
    public List<Position>? Positions { get; set; }

    /// <summary>
    /// Список льгот
    /// </summary>
    public List<Benefit>? Benefits { get; set; }

    /// <summary>
    /// Список записей о трудовой деятельности
    /// </summary>
    public List<WorkHistory>? WorkHistories { get; set; }

    /// <summary>
    /// Список данных о сотрудниках
    /// </summary>
    public List<EmployeeData>? EmployeeDatas { get; set; }

    /// <summary>
    /// Список профсоюзных организаций
    /// </summary>
    public List<Union>? Unions { get; set; }
}