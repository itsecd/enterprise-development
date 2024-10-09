using HRDepartment.Domain;
using Xunit;

namespace HRDepartment.UnitTests;

public class HRDepartmentTest : IClassFixture<HRDepartmentData>
{
    private readonly HRDepartmentData _fixture;
    public HRDepartmentTest(HRDepartmentData fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// 1. Тестирует вывод всех сотрудников выбранного отдела
    /// </summary>
    [Fact]
    public void TestDisplayAllEmployees()
    {
        uint departmentId = 1;

        var employeesInDepartment = _fixture.EmployeeWithDepartmentFilledFixture
            .Where(e => e.EmployeePositions.Any(ep => ep.Position.DepartmentId == departmentId))
            .ToList();

        Assert.True(employeesInDepartment.Count > 0);
    }

    /// <summary>
    /// 2. Тестирует вывод сотрудников, работающих в нескольких отделах, упорядоченных по ФИО
    /// </summary>
    [Fact]
    public void TestEmployeesInMultipleDepartments()
    {
        var employeesInMultipleDepartments = _fixture.EmployeeWithDepartmentFilledFixture
            .Where(e => e.EmployeePositions.GroupBy(ep => ep.Position.DepartmentId).Count() > 1)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ThenBy(e => e.Patronymic)
            .ToList();

        Assert.All(employeesInMultipleDepartments, e => Assert.True(e.EmployeePositions.GroupBy(ep => ep.Position.DepartmentId).Count() > 1));
    }

    /// <summary>
    /// 3. Тестирует вывод архива об увольнениях, включающего регистрационный номер, ФИО, дату рождения, отдел, занимаемую должность
    /// </summary>
    [Fact]
    public void TestArchiveOutputOnDismissals()
    {
        var thirdQuery = _fixture.EmployeePosition
            .Where(ep => ep.RetirementDate != null)
            .Select(ep => new
            {
                ep.Employee.RegNumber,
                ep.Employee.FirstName,
                ep.Employee.LastName,
                ep.Employee.Patronymic,
                ep.Employee.BirthDate,
                ep.Position.Department,
                ep.Position.Name
            })
            .ToList();

        Assert.Equal(3, thirdQuery.Count);
        var regNumbers = thirdQuery.Select(q => q.RegNumber).ToList();
        Assert.False(regNumbers.Contains("1234"));
        Assert.False(regNumbers.Contains("1324"));
        Assert.True(regNumbers.Contains("1423"));
        Assert.True(regNumbers.Contains("1243"));
    }

    /// <summary>
    /// 4. Тестирует вывод среднего возраста сотрудников в каждом отделе
    /// </summary>
    [Fact]
    public void TestMiddleAgeConclusion()
    {
        var employees = _fixture.EmployeeWithDepartmentFilledFixture;
        var fourthQuery =
            (from employee in employees
             from employeePosition in employee.EmployeePositions
             where employeePosition.Position != null
             select new
             {
                 EmployeeAge = ((DateTime.Now -
                                 employee.BirthDate).TotalDays / 365.2422),
                 DepartmentId = employeePosition.Position.DepartmentId
             }
             )
            .GroupBy(tuple => tuple.DepartmentId)
            .Select(grp => new
            {
                AverageAge = grp.Average(employee => employee.EmployeeAge),
                DepartmentId = grp.Key
            })
            .ToList();
        Assert.All(fourthQuery, x => Assert.True(x.AverageAge >= 0));
        Assert.Equal(2, fourthQuery.Count);
    }

    /// <summary>
    /// 5. Тестирует вывод сведений о сотрудниках, получавших в прошлом году льготные профсоюзные путевки (с запросом вида путевки)
    /// </summary>
    [Fact]
    public void TestEmployeesWithBenefits()
    {
         var employeesWithBenefits = _fixture.EmployeeBenefitList
        .Where(ebi => (DateTime.Now - ebi.IssueDate).TotalDays < 365)
        .Select(ebi => new
        {
            ebi.Employee.RegNumber,
            ebi.Employee.FirstName,
            ebi.Employee.LastName,
            ebi.BenefitType.Name
        })
        .ToList();

        Assert.Contains(employeesWithBenefits, e => e.RegNumber == "1234");
        Assert.Contains(employeesWithBenefits, e => e.RegNumber == "1324");
        Assert.Contains(employeesWithBenefits, e => e.RegNumber == "1423");
        Assert.Equal(3, employeesWithBenefits.Count);
    }

    /// <summary>
    /// 6. Тестирует вывод топ 5 сотрудников, имеющих наибольших стаж работы на предприятии
    /// </summary>
    [Fact]
    public void TestSixthQuery()
    {
        var employeesWithWorkExperience = _fixture.EmployeePosition
    .Select(e => new
    {
        e.Employee.RegNumber,
        e.EmploymentDate,
        DismissalDate = e.RetirementDate ?? DateTime.Now,
        e.Employee.FirstName,
        e.Employee.LastName
    })
    .GroupBy(e => new { e.RegNumber, e.FirstName, e.LastName })
    .Select(g => new
    {
        g.Key.RegNumber,
        g.Key.FirstName,
        g.Key.LastName,
        WorkExperience = g.Sum(e => (e.DismissalDate - e.EmploymentDate).TotalDays / 365.2422)
    })
    .OrderByDescending(e => e.WorkExperience)
    .Take(5)
    .ToList();

        Assert.Equal(5, employeesWithWorkExperience.Count);
        Assert.True(employeesWithWorkExperience[0].WorkExperience > 23);
        Assert.True(employeesWithWorkExperience[1].WorkExperience > 22);
        Assert.True(employeesWithWorkExperience[2].WorkExperience > 16);
        Assert.True(employeesWithWorkExperience[3].WorkExperience > 4);
        Assert.True(employeesWithWorkExperience[4].WorkExperience > 3);
        Assert.Equal("1423", employeesWithWorkExperience[0].RegNumber.ToString());
        Assert.Equal("1234", employeesWithWorkExperience[1].RegNumber.ToString());
        Assert.Equal("1243", employeesWithWorkExperience[2].RegNumber.ToString());
        Assert.Equal("1425", employeesWithWorkExperience[3].RegNumber.ToString());
        Assert.Equal("1324", employeesWithWorkExperience[4].RegNumber.ToString());
    }
}