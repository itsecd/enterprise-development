using HRDepartment.Domain;

namespace HRDepartment.Tests;

public class HRDepartment
{
    public HRData HRData { get; set; }

    public HRDepartment()
    {
        HRData = new HRData
        {
            Employees = new List<Employee>
            {
                new Employee(new EmployeeData { Id = 1, Surname = "Ivanov", Name = "Ivan", Patronymic = "Ivanovich", Position = "Software Engineer" }, null, null, null),
                new Employee(new EmployeeData { Id = 2, Surname = "Petrov", Name = "Petor", Patronymic = "Petrovich", Position = "Marketing Specialist" }, null, null, null),
                new Employee(new EmployeeData { Id = 3, Surname = "Voronova", Name = "Anna", Patronymic = "Andreevna", Position = "Financial Analyst" }, null, null, null),
                new Employee(new EmployeeData { Id = 4, Surname = "Sidorov", Name = "Oleg", Patronymic = "Evgenievich", Position = "Marketing Specialist" }, null, null, null),
                new Employee(new EmployeeData { Id = 5, Surname = "Koshmal", Name = "Svetlana", Patronymic = "Igorevna", Position = "Financial Analyst" }, null, null, null)
            },
            Departments = new List<Department>
            {
                new Department { Id = 1, Name = "HR Department" },
                new Department { Id = 2, Name = "IT Department" },
                new Department { Id = 3, Name = "Marketing Department" },
                new Department { Id = 4, Name = "Financial Department" },
                new Department { Id = 5, Name = "Sales Department" },
                new Department { Id = 6, Name = "Operations Department" }
            },
            Positions = new List<Position>
            {
                new Position { Id = 1, Name = "Software Engineer" },
                new Position { Id = 2, Name = "Marketing Specialist" },
                new Position { Id = 3, Name = "Financial Analyst" },
                new Position { Id = 4, Name = "Marketing Specialist" },
                new Position { Id = 5, Name = "Financial Analyst" }
            },
            Benefits = new List<Benefit>
            {
                new Benefit { Id = 1, Type = "Health Insurance", Employee = new Employee(new EmployeeData { Id = 1, Surname = "Ivanov", Name = "Ivan", Patronymic = "Ivanovich" }, new Department { Id = 1, Name = "HR Department" }, new Position { Id = 1, Name = "Software Engineer" }, null), Date = new DateTime(2020, 1, 1) },
                new Benefit { Id = 2, Type = "Life Insurance", Employee = new Employee(new EmployeeData { Id = 2, Surname = "Petrov", Name = "Petor", Patronymic = "Petrovich" }, new Department { Id = 2, Name = "IT Department" }, new Position { Id = 2, Name = "Marketing Specialist" }, null), Date = new DateTime(2020, 2, 1) },
                new Benefit { Id = 3, Type = "Dental Insurance", Employee = new Employee(new EmployeeData { Id = 3, Surname = "Voronova", Name = "Anna", Patronymic = "Andreevna" }, new Department { Id = 3, Name = "Marketing Department" }, new Position { Id = 3, Name = "Financial Analyst" }, null), Date = new DateTime(2020, 3, 1) },
                new Benefit { Id = 2, Type = "Life Insurance", Employee = new Employee(new EmployeeData { Id = 4, Surname = "Sidorov", Name = "Oleg", Patronymic = "Evgenievich" }, new Department { Id = 2, Name = "IT Department" }, new Position { Id = 2, Name = "Marketing Specialist" }, null), Date = new DateTime(2020, 5, 1) },
                new Benefit { Id = 3, Type = "Dental Insurance", Employee = new Employee(new EmployeeData { Id = 5, Surname = "Koshmal", Name = "Svetlana", Patronymic = "Igorevna" }, new Department { Id = 3, Name = "Marketing Department" }, new Position { Id = 3, Name = "Financial Analyst" }, null), Date = new DateTime(2020, 7, 1) },
            },
            WorkHistories = new List<WorkHistory>
            {
                new WorkHistory { Id = 1, Employee = HRData.Employees.First(e => e.Id == 1), Position = HRData.Positions.First(p => p.Id == 1), StartDate = new DateTime(2018, 1, 1), EndDate = null },
                new WorkHistory { Id = 2, Employee = HRData.Employees.First(e => e.Id == 2), Position = HRData.Positions.First(p => p.Id == 2), StartDate = new DateTime(2019, 1, 1), EndDate = null },
                new WorkHistory { Id = 3, Employee = HRData.Employees.First(e => e.Id == 3), Position = HRData.Positions.First(p => p.Id == 3), StartDate = new DateTime(2020, 1, 1), EndDate = null },
                new WorkHistory { Id = 4, Employee = HRData.Employees.First(e => e.Id == 4), Position = HRData.Positions.First(p => p.Id == 2), StartDate = new DateTime(2020, 5, 1), EndDate = null },
                new WorkHistory { Id = 5, Employee = HRData.Employees.First(e => e.Id == 5), Position = HRData.Positions.First(p => p.Id == 3), StartDate = new DateTime(2020, 7, 1), EndDate = null },
            },
            Unions = new List<Union>
            {
                new Union { Id = 1,
                    Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 1) },
                    Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 1) }
                },
                new Union { Id = 2,
                    Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 2) },
                    Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 2) }
                },
                new Union { Id = 3,
                    Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 3) },
                    Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 3) }
                },
                new Union { Id = 4,
                    Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 4) },
                    Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 2) }
                },
                new Union { Id = 5,
                    Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 5) },
                    Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 3) }
                },
            },
        };
    }
}