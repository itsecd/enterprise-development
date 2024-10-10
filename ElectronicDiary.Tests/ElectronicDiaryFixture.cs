using ElectronicDiary.Domain;

namespace ElectronicDiary.Tests;

/// <summary>
/// Подготовка тестовых данных
/// </summary>
public class ElectronicDiaryFixture
{
    public List<Grade> GradesList;

    public ElectronicDiaryFixture()
    {
        GradesList = ElectronicDiaryFileReader.ReadGrades(Path.Combine("Data", "data.csv"));
    }
}
