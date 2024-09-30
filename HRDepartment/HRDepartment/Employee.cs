namespace HRDepartment.Domain;

/// <summary>
/// Сотрудник компании
/// </summary>
public class Employee
{
    /// <summary>
    /// Уникальный идентификатор сотрудника
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фамилия сотрудника
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Отчество сотрудника
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Дата рождения сотрудника
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Пол сотрудника
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Дата поступления сотрудника на работу
    /// </summary>
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Список отделов, в которых работает сотрудник
    /// </summary>
    public List<Department> Departments { get; set; } = new List<Department>();

    /// <summary>
    /// Должность сотрудника
    /// </summary>
    public Position? Position { get; set; }

    /// <summary>
    /// Домашний адрес сотрудника
    /// </summary>
    public string HomeAddress { get; set; }

    /// <summary>
    /// Рабочий телефон сотрудника
    /// </summary>
    public string WorkPhone { get; set; }

    /// <summary>
    /// Домашний телефон сотрудника
    /// </summary>
    public string HomePhone { get; set; }

    /// <summary>
    /// Семейное положение сотрудника
    /// </summary>
    public string FamilyStatus { get; set; }

    /// <summary>
    /// Количество человек в семье сотрудника
    /// </summary>
    public int FamilyMembers { get; set; }

    /// <summary>
    /// Количество детей у сотрудника
    /// </summary>
    public int Children { get; set; }

    /// <summary>
    /// Членство сотрудника в профсоюзе
    /// </summary>
    public Union? Union { get; set; }

    /// <summary>
    /// Архив трудовой деятельности сотрудника
    /// </summary>
    public List<WorkHistory> WorkHistory { get; set; } = new List<WorkHistory>();
}