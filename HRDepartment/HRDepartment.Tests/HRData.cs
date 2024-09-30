#pragma encoding utf-8
using HRDepartment.Domain;

namespace HRDepartment.Tests;

/// <summary>
/// �����, ������� ������������ ������ ��� ������������ 
/// </summary>
public class HRData
{
    /// <summary>
    /// ������ �����������
    /// </summary>
    public List<Employee>? Employees { get; set; }

    /// <summary>
    /// ������ �������
    /// </summary>
    public List<Department>? Departments { get; set; }

    /// <summary>
    /// ������ ����������
    /// </summary>
    public List<Position>? Positions { get; set; }

    /// <summary>
    /// ������ �����
    /// </summary>
    public List<Benefit>? Benefits { get; set; }

    /// <summary>
    /// ������ ������� � �������� ������������
    /// </summary>
    public List<WorkHistory>? WorkHistories { get; set; }

    /// <summary>
    /// ������ ����������� �����������
    /// </summary>
    public List<Union>? Unions { get; set; }
}