using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDepartment.Domain;

namespace HRDepartment.UnitTests; 

public class HRDepartmentData
{
    /// <summary>
    /// Список цехов
    /// </summary>
    public List<Workshop> Workshop
    {
        get
        {
            return new List<Workshop>
            {
                new Workshop
                {
                    Name = "Цех 1",
                    Id = 1,
                    Employees = new List<Employee>()
                },
                new Workshop
                {
                    Name = "Цех 2",
                    Id = 2,
                    Employees = new List<Employee>()
                },
                new Workshop {
                    Name = "Цех 3",
                    Id = 3,
                    Employees = new List<Employee>()
                },
                new Workshop
                {
                    Name = "Цех 4",
                    Id = 4,
                    Employees = new List<Employee>()
                },
                new Workshop
                {
                    Name = "Цех 5",
                    Id = 5,
                    Employees = new List<Employee>()
                },
                new Workshop
                {
                    Name = "Цех 6",
                    Id = 6,
                    Employees = new List<Employee>()
                }
            };
        }
    }
    /// <summary>
    /// Список отделов
    /// </summary>
    public List<Department> Department
    {
        get
        {
            return new List<Department>
            {
                new Department
                {
                    Name = "Отдел 1",
                    Id = 1
                },
                new Department
                {
                    Name = "Отдел 2",
                    Id = 2
                },
                new Department
                {
                    Name = "Отдел 3",
                    Id = 3
                },
                new Department
                {
                    Name = "Отдел 4",
                    Id = 4
                },
                new Department
                {
                    Name = "Отдел 5",
                    Id = 5
                },
                new Department
                {
                    Name = "Отдел 6",
                    Id = 6
                },
                new Department
                {
                    Name = "Отдел 7",
                    Id = 7
                },
                new Department
                {
                    Name = "Отдел 8",
                    Id = 8
                },
                new Department
                {
                    Name = "Отдел 9",
                    Id = 9
                },
                new Department
                {
                    Name = "Отдел 10",
                    Id = 10
                }
            };
        }
    }
    /// <summary>
    /// Список должностей
    /// </summary>
    public List<Position> Position
    {
        get
        {
            return new List<Position>
        {
            new Position
            {
                Name = "Аналитик данных",
                Id = 1,
                DepartmentId = 1,
                Department = null,
                EmployeePositions = new List<EmployeePosition>()
            },
            new Position
            {
                Name = "Фронтенд-разработчик",
                Id = 2,
                DepartmentId = 1,
                Department = null,
                EmployeePositions = new List<EmployeePosition>()
            },
            new Position
            {
                Name = "Тестировщик",
                Id = 3,
                DepartmentId = 2,
                Department = null,
                EmployeePositions = new List<EmployeePosition>()
            },
            new Position
            {
                Name = "1С разработчик",
                Id = 4,
                DepartmentId = 2,
                Department = null,
                EmployeePositions = new List<EmployeePosition>()
            },
            new Position
            {
                Name = "Аналитик 1С",
                Id = 5,
                DepartmentId = 3,
                Department = null,
                EmployeePositions = new List<EmployeePosition>()
            }
        };
        }
    }
    /// <summary>
    /// Список типов льгот с пустыми списками отпускных льгот
    /// </summary>
    public List<BenefitType> BenefitType
    {
        get
        {
            return new List<BenefitType>
        {
            new BenefitType
            {
                Id = 1,
                Name = "Льгота 1",
                EmployeeBenefits = new List<EmployeeBenefit>()
            },
            new BenefitType
            {
                Id = 2,
                Name = "Льгота 2",
                EmployeeBenefits = new List<EmployeeBenefit>()
            },
            new BenefitType
            {
                Id = 3,
                Name = "Льгота 3",
                EmployeeBenefits = new List<EmployeeBenefit>()
            }
        };
        }
    }
    /// <summary>
    /// Список сотрудников с заполненными цехами
    /// </summary>
    public List<Employee> EmployeeOnlyWorkshopFilledFixture
    {
        get
        {
            var workshopList = Workshop;
            var employeeList = new List<Employee>()
            {
                new Employee
                {
                    Id = 0,
                    RegNumber = "1234",
                    FirstName = "Иванов",
                    LastName = "Иван",
                    Patronymic = "Иванович",
                    BirthDate = new DateTime(1978, 3, 22),
                    WorkshopId = 1,
                    Workshop = null,
                    WorkPhoneNumber = "1234567890",
                    PersonalPhoneNumber = "0987654321",
                    Address = "г.Самара, ул.Льва Толстого д.35",
                    FamilyMembersCount = 4,
                    ChildrenCount = 2,
                    MaritalStatus = "женат",
                    EmployeePositions = new List<EmployeePosition>(),
                    EmployeeBenefits = new List<EmployeeBenefit>()
                },
                new Employee
                {
                    Id = 1,
                    RegNumber = "1324",
                    FirstName = "Петров",
                    LastName = "Петр",
                    Patronymic = "Пертович",
                    BirthDate = new DateTime(2000, 1, 23),
                    WorkshopId = 2,
                    Workshop = null,
                    WorkPhoneNumber = "0987654321",
                    PersonalPhoneNumber = "1234567890",
                    Address = "г.Самара, ул.Ленина, д.47",
                    FamilyMembersCount = 3,
                    ChildrenCount = 0,
                    MaritalStatus = "холост",
                    EmployeePositions = new List<EmployeePosition>(),
                    EmployeeBenefits = new List<EmployeeBenefit>()
                },
                new Employee
                {
                    Id = 2,
                    RegNumber = "1423",
                    FirstName = "Воронова",
                    LastName = "Анна",
                    Patronymic = "Андреевна",
                    BirthDate = new DateTime(1978, 8, 6),
                    WorkshopId = 3,
                    Workshop = null,
                    WorkPhoneNumber = "1234567890",
                    PersonalPhoneNumber = "0987654321",
                    Address = "г.Самара Московское шоссе, д.65",
                    FamilyMembersCount = 5,
                    ChildrenCount = 3,
                    MaritalStatus = "замужем",
                    EmployeePositions = new List<EmployeePosition>(),
                    EmployeeBenefits = new List<EmployeeBenefit>()
                },
                new Employee
                {
                    Id = 3,
                    RegNumber = "1243",
                    FirstName = "Кошмал",
                    LastName = "Светлана",
                    Patronymic = "Игоревна",
                    BirthDate = new DateTime(1980, 10, 10),
                    WorkshopId = 4,
                    Workshop = null,
                    WorkPhoneNumber = "1234567890",
                    PersonalPhoneNumber = "0987654321",
                    Address = "г. Самара, ул.Запорожская д.43А",
                    FamilyMembersCount = 3,
                    ChildrenCount = 1,
                    MaritalStatus = "замужем",
                    EmployeePositions = new List<EmployeePosition>(),
                    EmployeeBenefits = new List<EmployeeBenefit>()
                },
               new Employee
                {
                    Id = 4,
                    RegNumber = "1425",
                    FirstName = "Сидоров",
                    LastName = "Олег",
                    Patronymic = "Евгеньевич",
                    BirthDate = new DateTime(1988, 10, 9),
                    WorkshopId = 5,
                    Workshop = null,
                    WorkPhoneNumber = "1234567890",
                    PersonalPhoneNumber = "0987654321",
                    Address = "г. Самара, ул.Вольская д.43",
                    FamilyMembersCount = 3,
                    ChildrenCount = 1,
                    MaritalStatus = "женат",
                    EmployeePositions = new List<EmployeePosition>(),
                    EmployeeBenefits = new List<EmployeeBenefit>()
                }
            };
            workshopList[0].Employees.Add(employeeList[4]);
            workshopList[1].Employees.Add(employeeList[3]);
            workshopList[2].Employees.Add(employeeList[2]);
            workshopList[3].Employees.Add(employeeList[1]);
            workshopList[4].Employees.Add(employeeList[0]);
            employeeList[4].Workshop = workshopList[0];
            employeeList[3].Workshop = workshopList[1];
            employeeList[2].Workshop = workshopList[2];
            employeeList[1].Workshop = workshopList[3];
            employeeList[0].Workshop = workshopList[4];
            return employeeList;
        }
    }
    /// <summary>
    /// Cоздает список сотрудников и позиций, а затем связывает их друг с другом
    /// </summary>
    public List<EmployeePosition> EmployeePosition
    {
        get
        {
            var employeeList = EmployeeOnlyWorkshopFilledFixture;
            var positionList = Position;
            var employeePositionList = new List<EmployeePosition>
        {
            new EmployeePosition
            {
                Id = 1,
                EmployeeId = employeeList[0].Id,
                Employee = employeeList[0],
                PositionId = positionList[1].Id,
                Position = positionList[1],
                EmploymentDate = new DateTime(2000, 1, 27),
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 2,
                EmployeeId = employeeList[2].Id,
                Employee = employeeList[2],
                PositionId = positionList[4].Id,
                Position = positionList[4],
                EmploymentDate = new DateTime(1998, 3, 20),
                RetirementDate = new DateTime(2010, 5, 23)
            },
            new EmployeePosition
            {
                Id = 3,
                EmployeeId = employeeList[2].Id,
                Employee = employeeList[2],
                PositionId = positionList[1].Id,
                Position = positionList[1],
                EmploymentDate = new DateTime(2010, 5, 24),
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 4,
                EmployeeId = employeeList[1].Id,
                Employee = employeeList[1],
                PositionId = positionList[2].Id,
                Position = positionList[2],
                EmploymentDate = new DateTime(2018, 8, 7),
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 5,
                EmployeeId = employeeList[3].Id,
                Employee = employeeList[3],
                PositionId = positionList[1].Id,
                Position = positionList[1],
                EmploymentDate = new DateTime(2000, 9, 10),
                RetirementDate = new DateTime(2011, 11, 11)
            },
            new EmployeePosition
            {
                Id = 6,
                EmployeeId = employeeList[3].Id,
                Employee = employeeList[3],
                PositionId = positionList[3].Id,
                Position = positionList[3],
                EmploymentDate = new DateTime(2011, 11, 12),
                RetirementDate = new DateTime(2016, 3, 24)
            },
            new EmployeePosition
            {
                Id = 7,
                EmployeeId = employeeList[3].Id,
                Employee = employeeList[3],
                PositionId = positionList[4].Id,
                Position = positionList[4],
                EmploymentDate = new DateTime(2016, 3, 25),
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 8,
                EmployeeId = employeeList[4].Id,
                Employee = employeeList[4],
                PositionId = positionList[2].Id,
                Position = positionList[2],
                EmploymentDate = new DateTime(2005, 1, 1),
                RetirementDate = null
            }
        };
            employeeList[0].EmployeePositions.Add(employeePositionList[0]);
            employeeList[2].EmployeePositions.Add(employeePositionList[1]);
            employeeList[2].EmployeePositions.Add(employeePositionList[2]);
            employeeList[1].EmployeePositions.Add(employeePositionList[3]);
            employeeList[3].EmployeePositions.Add(employeePositionList[4]);
            employeeList[3].EmployeePositions.Add(employeePositionList[5]);
            employeeList[3].EmployeePositions.Add(employeePositionList[6]);
            employeeList[4].EmployeePositions.Add(employeePositionList[7]);
            positionList[1].EmployeePositions.Add(employeePositionList[0]);
            positionList[1].EmployeePositions.Add(employeePositionList[2]);
            positionList[1].EmployeePositions.Add(employeePositionList[4]);
            positionList[4].EmployeePositions.Add(employeePositionList[1]);
            positionList[4].EmployeePositions.Add(employeePositionList[6]);
            positionList[2].EmployeePositions.Add(employeePositionList[3]);
            positionList[2].EmployeePositions.Add(employeePositionList[7]);
            positionList[3].EmployeePositions.Add(employeePositionList[5]);
            return employeePositionList;
        }
    }
    public List<Employee> EmployeeWithDepartmentFilledFixture
    {
        get
        {
            var departmentList = Department;
            var employees = EmployeeOnlyWorkshopFilledFixture;
            var positionList = Position;
            var employeePositionList = new List<EmployeePosition>
        {
            new EmployeePosition
            {
                Id = 1,
                EmployeeId = employees[0].Id,
                Employee = employees[0],
                PositionId = positionList[0].Id,
                Position = positionList[0],
                EmploymentDate = DateTime.Now,
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 2,
                EmployeeId = employees[1].Id,
                Employee = employees[1],
                PositionId = positionList[1].Id,
                Position = positionList[1],
                EmploymentDate = DateTime.Now,
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 3,
                EmployeeId = employees[2].Id,
                Employee = employees[2],
                PositionId = positionList[2].Id,
                Position = positionList[2],
                EmploymentDate = DateTime.Now,
                RetirementDate = null
            },
            new EmployeePosition
            {
                Id = 4,
                EmployeeId = employees[3].Id,
                Employee = employees[3],
                PositionId = positionList[3].Id,
                Position = positionList[3],
                EmploymentDate = DateTime.Now,
                RetirementDate = null
            }
        };
            employees[0].EmployeePositions.Add(employeePositionList[0]);
            employees[1].EmployeePositions.Add(employeePositionList[1]);
            employees[2].EmployeePositions.Add(employeePositionList[2]);
            employees[3].EmployeePositions.Add(employeePositionList[3]);
            positionList[0].EmployeePositions.Add(employeePositionList[0]);
            positionList[1].EmployeePositions.Add(employeePositionList[1]);
            positionList[2].EmployeePositions.Add(employeePositionList[2]);
            positionList[3].EmployeePositions.Add(employeePositionList[3]);
            return employees;
        }
    }
    /// <summary>
    /// Список отпускных с заполненным типом льгот
    /// </summary>
    public List<EmployeeBenefit> EmployeeBenefit
    {
        get
        {
            var employees = EmployeeOnlyWorkshopFilledFixture;
            var benefitTypes = BenefitType;
            var employeeBenefitList = new List<EmployeeBenefit>()
        {
            new EmployeeBenefit
            {
                Id = 1,
                EmployeeId = employees[0].Id,
                Employee = employees[0],
                BenefitTypeId = benefitTypes[0].Id,
                BenefitType = benefitTypes[0],
                IssueDate = DateTime.Now.AddMonths(-6)
            },
            new EmployeeBenefit
            {
                Id = 2,
                EmployeeId = employees[1].Id,
                Employee = employees[1],
                BenefitTypeId = benefitTypes[1].Id,
                BenefitType = benefitTypes[1],
                IssueDate = DateTime.Now.AddMonths(-3)
            },
            new EmployeeBenefit
            {
                Id = 3,
                EmployeeId = employees[2].Id,
                Employee = employees[2],
                BenefitTypeId = benefitTypes[2].Id,
                BenefitType = benefitTypes[2],
                IssueDate = DateTime.Now
            }
        };
            employees[0].EmployeeBenefits.Add(employeeBenefitList[0]);
            employees[1].EmployeeBenefits.Add(employeeBenefitList[1]);
            employees[2].EmployeeBenefits.Add(employeeBenefitList[2]);
            benefitTypes[0].EmployeeBenefits.Add(employeeBenefitList[0]);
            benefitTypes[1].EmployeeBenefits.Add(employeeBenefitList[1]);
            benefitTypes[2].EmployeeBenefits.Add(employeeBenefitList[2]);
            return employeeBenefitList;
        }
    }
    /// <summary>
    /// Список льгот сотрудников с заполненными льготами сотрудников и отпусков
    /// </summary>
    public List<EmployeeBenefit> EmployeeBenefitList
    {
        get
        {
            var employees = EmployeeOnlyWorkshopFilledFixture;
            var benefitTypes = BenefitType;
            var employeeBenefitList = new List<EmployeeBenefit>()
        {
            new EmployeeBenefit
            {
                Id = 1,
                EmployeeId = employees[0].Id,
                Employee = employees[0],
                BenefitTypeId = benefitTypes[0].Id,
                BenefitType = benefitTypes[0],
                IssueDate = DateTime.Now
            },
            new EmployeeBenefit
            {
                Id = 2,
                EmployeeId = employees[1].Id,
                Employee = employees[1],
                BenefitTypeId = benefitTypes[1].Id,
                BenefitType = benefitTypes[1],
                IssueDate = DateTime.Now
            },
            new EmployeeBenefit
            {
                Id = 3,
                EmployeeId = employees[2].Id,
                Employee = employees[2],
                BenefitTypeId = benefitTypes[2].Id,
                BenefitType = benefitTypes[2],
                IssueDate = DateTime.Now
            }
        };
            employees[0].EmployeeBenefits.Add(employeeBenefitList[0]);
            employees[1].EmployeeBenefits.Add(employeeBenefitList[1]);
            employees[2].EmployeeBenefits.Add(employeeBenefitList[2]);
            benefitTypes[0].EmployeeBenefits.Add(employeeBenefitList[0]);
            benefitTypes[1].EmployeeBenefits.Add(employeeBenefitList[1]);
            benefitTypes[2].EmployeeBenefits.Add(employeeBenefitList[2]);
            return employeeBenefitList;
        }
    }
}