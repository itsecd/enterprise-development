using HRDepartment.Domain;

namespace HRDepartment.Tests;

/// <summary>
/// Данные 
/// </summary>
public class HRData
{
    /// <summary>
    /// 
    /// </summary>
    public List<Employee>? Employees { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Department>? Departments { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Position>? Positions { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Benefit>? Benefits { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<WorkHistory>? WorkHistories { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<EmployeeData>? EmployeeDatas { get; set; }
    public List<Union>? Unions { get; set; }
}