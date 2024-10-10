namespace ElectronicDiary.Domain;

public class Student
{
    /// <summary>
    /// Идентификатор студента
    /// </summary>
    public required int IdStudent { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string Surname { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public required string Patronymic { get; set; }
    /// <summary>
    /// Идентификатор класса
    /// </summary>
    public required Class Class { get; set; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly Birthday { get; set; }
    /// <summary>
    /// Пасспорт
    /// </summary>
    public required string Passport { get; set; }
}
