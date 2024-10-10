namespace ElectronicDiary.Domain;

public class Grade
{
    /// <summary>
    /// Идентификатор оценки
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Предмет
    /// </summary>
    public required Subject Subject { get; set; }
    /// <summary>
    /// Ученик
    /// </summary>
    public required Student Student { get; set; }
    /// <summary>
    /// Оценка
    /// </summary>
    public required GradeTypes GradeValue { get; set; }
    /// <summary>
    /// Дата получения
    /// </summary>
    public required DateOnly Date { get; set; }
}
