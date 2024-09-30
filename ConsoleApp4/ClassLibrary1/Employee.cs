namespace HRDepartment.Domain;

/// <summary>
/// Сотрудник компании
/// </summary>
public class Employee(EmployeeData data, Department department, Position position, Union union)
{
    /// <summary>
    /// Уникальный идентификатор сотрудника
    /// </summary>
    public int Id { get; set; } = data.Id;

    /// <summary>
    /// Фамилия сотрудника
    /// </summary>
    public string Surname { get; set; } = data.Surname;

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    public string Name { get; set; } = data.Name;

    /// <summary>
    /// Отчество сотрудника
    /// </summary>
    public string Patronymic { get; set; } = data.Patronymic;

    /// <summary>
    /// Дата рождения сотрудника
    /// </summary>
    public DateTime BirthDate { get; set; } = data.BirthDate;

    /// <summary>
    /// Пол сотрудника
    /// </summary>
    public string Gender { get; set; } = data.Gender;

    /// <summary>
    /// Дата поступления сотрудника на работу
    /// </summary>
    public DateTime HireDate { get; set; } = data.HireDate;

    /// <summary>
    /// Список отделов, в которых работает сотрудник
    /// </summary>
    public List<Department> Departments { get; set; } = department != null ? new List<Department> { department } : new List<Department>();

    /// <summary>
    /// Должность сотрудника
    /// </summary>
    public Position? Position { get; set; } = position;

    /// <summary>
    /// Домашний адрес сотрудника
    /// </summary>
    public string HomeAddress { get; set; } = data.HomeAddress;

    /// <summary>
    /// Рабочий телефон сотрудника
    /// </summary>
    public string WorkPhone { get; set; } = data.WorkPhone;

    /// <summary>
    /// Домашний телефон сотрудника
    /// </summary>
    public string HomePhone { get; set; } = data.HomePhone;

    /// <summary>
    /// Семейное положение сотрудника
    /// </summary>
    public string FamilyStatus { get; set; } = data.FamilyStatus;

    /// <summary>
    /// Количество человек в семье сотрудника
    /// </summary>
    public int FamilyMembers { get; set; } = data.FamilyMembers;

    /// <summary>
    /// Количество детей у сотрудника
    /// </summary>
    public int Children { get; set; } = data.Children;

    /// <summary>
    /// Членство сотрудника в профсоюзе
    /// </summary>
    public Union? Union { get; set; } = union;

    /// <summary>
    /// Архив трудовой деятельности сотрудника
    /// </summary>
    public List<WorkHistory> WorkHistory { get; set; } = new List<WorkHistory>();
}