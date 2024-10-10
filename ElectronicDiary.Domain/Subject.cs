namespace ElectronicDiary.Domain;

public class Subject
{
    /// <summary>
    /// Идентификатор предмета
    /// </summary>
    public required int IdSubject { get; set; }
    /// <summary>
    /// Название предмета
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Год обучения
    /// </summary>
    public required string StudyYear { get; set; }
}
