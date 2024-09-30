using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;

/// <summary>
/// Класс данных о сотруднике
/// </summary>
public class EmployeeData
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
    /// Должность сотрудника
    /// </summary>
    public string Position { get; set; }
}