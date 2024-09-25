namespace HRDepartment.Domain;

/// <summary>
/// Сотрудник
/// </summary>
public class Employee(EmployeeData data, Department department, Position position, Union union)
{
    /// <summary>
    /// Регистрационный номер
    /// </summary>
    public int Id { get; set; } = data.Id;
    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; } = data.Surname;
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = data.Name;
    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; } = data.Patronymic;
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; } = data.BirthDate;
    /// <summary>
    /// Пол
    /// </summary>
    public string Gender { get; set; } = data.Gender;
    /// <summary>
    /// Дата поступления
    /// </summary>
    public DateTime HireDate { get; set; } = data.HireDate;
    /// <summary>
    /// Отдел
    /// </summary>
    public List<Department> Departments { get; set; } = new List<Department> { department };
    /// <summary>
    /// Должность
    /// </summary>
    public Position Position { get; set; } = position;
    /// <summary>
    /// Домашний адресс
    /// </summary>
    public string HomeAddress { get; set; } = data.HomeAddress;
    /// <summary>
    /// Рабочий телефон
    /// </summary>
    public string WorkPhone { get; set; } = data.WorkPhone;
    /// <summary>
    /// Домашний телефон
    /// </summary>
    public string HomePhone { get; set; } = data.HomePhone;
    /// <summary>
    /// Семпейное положение
    /// </summary>
    public string FamilyStatus { get; set; } = data.FamilyStatus;
    /// <summary>
    /// Число человек в семье
    /// </summary>
    public int FamilyMembers { get; set; } = data.FamilyMembers;
    /// <summary>
    /// Количество детей
    /// </summary>
    public int Children { get; set; } = data.Children;
    /// <summary>
    /// Членство в профсоюзе
    /// </summary>
    public Union Union { get; set; } = union;
    /// <summary>
    /// Фрхив трудовой деятельности
    /// </summary>
    public List<WorkHistory> WorkHistory { get; set; } = new List<WorkHistory>();
    /// <summary>
    /// Полное имя сотрудника для удобства
    /// </summary>
    public string FullName => $"{Surname} {Name} {Patronymic}";
}