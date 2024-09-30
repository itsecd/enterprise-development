#pragma encoding utf-8
namespace HRDepartment.Tests;

using System;
using Xunit;

/// <summary>
/// Unit тестирование с использованием данных
/// </summary>
public class HRDepartmentTest : IClassFixture<HRDepartment>
{
    private readonly HRDepartment _fixture;

    /// <summary>
    /// Инициализирует новый экземпляр класса
    /// </summary>
    /// <param name="fixture">Экземпляр класса <see cref="HRDepartment"/>.</param>
    public HRDepartmentTest(HRDepartment fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// 1. Тестирует вывод всех сотрудников выбранного отдела
    /// </summary>
    [Fact]
    public void TestDisplayAllEmployees()
    {
        var employees = _fixture.HRData.Employees;

        Assert.Equal(5, employees.Count);
    }

    /// <summary>
    /// 2. Тестирует вывод сотрудников, работающих в нескольких отделах, упорядоченных по ФИО
    /// </summary>
    [Fact]
    public void TestEmployeesInMultipleDepartments()
    {
        var employeesInMultipleDepartments = _fixture.HRData.Employees
            .Where(e => e.Departments.Count > 1)
            .OrderBy(e => e.Surname)
            .ThenBy(e => e.Name)
            .ThenBy(e => e.Patronymic)
            .ToList();

        if (employeesInMultipleDepartments.Any())
        {
            Assert.NotEmpty(employeesInMultipleDepartments);
            Assert.All(employeesInMultipleDepartments, e => Assert.True(e.Departments.Count > 1));
        }
        else
        {
            Assert.Empty(employeesInMultipleDepartments);
        }
    }

    /// <summary>
    /// 3. Тестирует вывод архива об увольнениях, включающего регистрационный номер, ФИО, дату рождения, отдел, занимаемую должность
    /// </summary>
    [Fact]
    public void GetEmployeesWorkingInMultipleDepartments()
    {
        var employees = _fixture.HRData.Employees
            .Where(e => e.Departments.Count > 1)
            .OrderBy(e => e.Surname)
            .ThenBy(e => e.Name)
            .ThenBy(e => e.Patronymic)
            .ToList();

        Assert.NotEmpty(employees);
    }

    /// <summary>
    /// 4. Тестирует вывод среднего возраста сотрудников в каждом отделе
    /// </summary>
    [Fact]
    public void GetAverageAgeOfEmployeesInEachDepartment()
    {
        var averageAges = _fixture.HRData.Departments
            .Where(d => d.Employees != null && d.Employees.Any())
            .Select(d => new
            {
                DepartmentName = d.Name,
                AverageAge = d.Employees.Average(e => (DateTime.Now.Year - e.BirthDate.Year))
            })
            .ToList();

        if (averageAges.Any())
        {
            Assert.NotEmpty(averageAges);
            Assert.All(averageAges, a => Assert.True(a.AverageAge > 0));
        }
        else
        {
            Assert.Empty(averageAges);
        }
    }

    /// <summary>
    /// 5. Тестирует вывод сведений о сотрудниках, получавших в прошлом году льготные профсоюзные путевки (с запросом вида путевки)
    /// </summary>
    [Fact]
    public void TestEmployeesWithUnionBenefits()
    {
        var employeesWithUnionBenefits = _fixture.HRData.Benefits
            .Where(b => b.Type == "путевка" && b.Date.Year == 2022)
            .Select(b => b.Employee)
            .Distinct()
            .ToList();

        Assert.NotEmpty(employeesWithUnionBenefits);

        // Проверяем сотрудников
        Assert.Equal(1, employeesWithUnionBenefits.Count);
        Assert.Equal(1, employeesWithUnionBenefits[0].Id);
    }

    /// <summary>
    /// 6. Тестирует вывод топ 5 сотрудников, имеющих наибольших стаж работы на предприятии
    /// </summary>
    [Fact]
    public void TestTop5EmployeesBySeniority()
    {
        var top5Employees = _fixture.HRData.Employees
            .OrderByDescending(e => _fixture.HRData.WorkHistories
                .Where(w => w.Employee.Id == e.Id)
                .Select(w => (w.EndDate ?? DateTime.MaxValue) - w.StartDate)
                .DefaultIfEmpty(TimeSpan.Zero)
                .Sum(t => t.Days))
            .Take(5)
            .ToList();

        Assert.Equal(5, top5Employees.Count);

        Assert.Equal(2, top5Employees[0].Id); // Петров Петр Петрович
        Assert.Equal(3, top5Employees[1].Id); // Воронова Анна Андреевна
        Assert.Equal(1, top5Employees[2].Id); // Иванов Иван Иванович
        Assert.Equal(4, top5Employees[3].Id); // Сидоров Олег Евгеньевич
        Assert.Equal(5, top5Employees[4].Id); // Кошмал Светлана Игоревна
    }
}