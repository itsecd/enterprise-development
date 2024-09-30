using HRDepartment.Domain;

namespace HRDepartment.Tests;

/// <summary>
/// Тестовые данные о сотрудниках
/// </summary>
public class HRDepartment
{
    /// <summary>
    /// Объявление сущностей и связей 
    /// 1. Сотрудник - Отдел(многие-ко-многим),
    /// 2. Сотрудник - Должность(многие-ко-многим),
    /// 3. Сотрудник - Льгота(многие-ко-многим),
    /// 4. Сотрудник - История работы(один-ко-многим),
    /// 5.  Сотрудник - Профсоюз(многие-ко-многим).
    /// </summary>
    public HRData HRData { get; set; }

    /// <summary>
    /// Заполнение тестовых списков исходными данными
    /// </summary>
    public HRDepartment()
    {
        HRData = new HRData();
        InitializeHRData();
    }

    private void InitializeHRData()
    {   /// <summary>
        /// Инициализация сотрудников
        /// </summary>
        HRData.Employees = new List<Employee>
        {
            new Employee(new EmployeeData { Id = 1, Surname = "Ivanov", Name = "Ivan", Patronymic = "Ivanovich", BirthDate = new DateTime(1990, 1, 1), Position = "Software Engineer" }, new Department { Id = 1, Name = "HR Department" }, null, null),
            new Employee(new EmployeeData { Id = 2, Surname = "Petrov", Name = "Petor", Patronymic = "Petrovich", BirthDate = new DateTime(1991, 1, 1), Position = "Marketing Specialist" }, new Department { Id = 2, Name = "IT Department" }, null, null),
            new Employee(new EmployeeData { Id = 3, Surname = "Voronova", Name = "Anna", Patronymic = "Andreevna", BirthDate = new DateTime(1992, 1, 1), Position = "Financial Analyst" }, new Department { Id = 3, Name = "Marketing Department" }, null, null),
            new Employee(new EmployeeData { Id = 4, Surname = "Sidorov", Name = "Oleg", Patronymic = "Evgenievich", BirthDate = new DateTime(1993, 1, 1), Position = "Marketing Specialist" }, new Department { Id = 2, Name = "IT Department" }, null, null),
            new Employee(new EmployeeData { Id = 5, Surname = "Koshmal", Name = "Svetlana", Patronymic = "Igorevna", BirthDate = new DateTime(1994, 1, 1), Position = "Financial Analyst" }, new Department { Id = 3, Name = "Marketing Department" }, null, null)
        };

        /// <summary>
        /// Инициализация отделов
        /// </summary>
        HRData.Departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR Department" },
            new Department { Id = 2, Name = "IT Department" },
            new Department { Id = 3, Name = "Marketing Department" },
            new Department { Id = 4, Name = "Financial Department" },
            new Department { Id = 5, Name = "Sales Department" },
            new Department { Id = 6, Name = "Operations Department" }
        };

        /// <summary>
        /// Добавляем сотрудников в несколько отделов
        /// </summary>
        HRData.Employees.First(e => e.Id == 1).Departments.Add(new Department { Id = 2, Name = "IT Department" });
        HRData.Employees.First(e => e.Id == 2).Departments.Add(new Department { Id = 3, Name = "Marketing Department" });
        HRData.Employees.First(e => e.Id == 3).Departments.Add(new Department { Id = 1, Name = "HR Department" });

        /// <summary>
        /// Инициализация должностей
        /// </summary>
        HRData.Positions = new List<Position>
        {
            new Position { Id = 1, Name = "Software Engineer" },
            new Position { Id = 2, Name = "Marketing Specialist" },
            new Position { Id = 3, Name = "Financial Analyst" },
            new Position { Id = 4, Name = "Marketing Specialist" },
            new Position { Id = 5, Name = "Financial Analyst" }
        };

        /// <summary>
        /// Инициализация льгот
        /// </summary>
        HRData.Benefits = new List<Benefit>
        {
            new Benefit { Id = 1, Type = "Health Insurance", Employee = HRData.Employees.First(e => e.Id == 1), Date = new DateTime(2020, 1, 1) },
            new Benefit { Id = 2, Type = "Life Insurance", Employee = HRData.Employees.First(e => e.Id == 2), Date = new DateTime(2020, 2, 1) },
            new Benefit { Id = 3, Type = "Dental Insurance", Employee = HRData.Employees.First(e => e.Id == 3), Date = new DateTime(2020, 3, 1) },
            new Benefit { Id = 2, Type = "Life Insurance", Employee = HRData.Employees.First(e => e.Id == 4), Date = new DateTime(2020, 5, 1) },
            new Benefit { Id = 3, Type = "Dental Insurance", Employee = HRData.Employees.First(e => e.Id == 5), Date = new DateTime(2020, 7, 1) },
            new Benefit { Id = 6, Type = " путевка", Employee = HRData.Employees.First(e => e.Id == 1), Date = new DateTime(2022, 1, 1) },
        };

        /// <summary>
        /// Инициализация истории работы
        /// </summary>
        HRData.WorkHistories = new List<WorkHistory>
        {
            new WorkHistory { Id = 1, Employee = HRData.Employees.First(e => e.Id == 1), Position = HRData.Positions.First(p => p.Id == 1), StartDate = new DateTime(2010, 1, 1), EndDate = new DateTime(2014, 1, 1) },
            new WorkHistory { Id = 2, Employee = HRData.Employees.First(e => e.Id == 2), Position = HRData.Positions.First(p => p.Id == 2), StartDate = new DateTime(2012, 1, 1), EndDate = new DateTime(2018, 1, 1) },
            new WorkHistory { Id = 3, Employee = HRData.Employees.First(e => e.Id == 3), Position = HRData.Positions.First(p => p.Id == 3), StartDate = new DateTime(2015, 1, 1), EndDate = new DateTime(2020, 1, 1) },
            new WorkHistory { Id = 4, Employee = HRData.Employees.First(e => e.Id == 4), Position = HRData.Positions.First(p => p.Id == 2), StartDate = new DateTime(2017, 1, 1), EndDate = new DateTime(2020, 1, 1) },
            new WorkHistory { Id = 5, Employee = HRData.Employees.First(e => e.Id == 5), Position = HRData.Positions.First(p => p.Id == 3), StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2022, 1, 1) },
        };

        /// <summary>
        /// Инициализация профсоюзов
        /// </summary>
        HRData.Unions = new List<Union>
        {
            new Union { Id = 1, Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 1) }, Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 1) } },
            new Union { Id = 2, Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 2) }, Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 2) } },
            new Union { Id = 3, Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 3) }, Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 3) } },
            new Union { Id = 4, Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 4) }, Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 2) } },
            new Union { Id = 5, Employees = new List<Employee> { HRData.Employees.First(e => e.Id == 5) }, Benefits = new List<Benefit> { HRData.Benefits.First(b => b.Id == 3) } },
        };
    }
}