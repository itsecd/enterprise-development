using ElectronicDiary.Domain;

namespace ElectronicDiary.Tests;

public class ElectronicDiaryTests(ElectronicDiaryFixture fixture) : IClassFixture<ElectronicDiaryFixture>
{
    private readonly ElectronicDiaryFixture _fixture = fixture;

    /// <summary>
    /// Тест проверяет, что в списке предметов содержатся все ожидаемые предметы,
    /// такие как Biology, Literature, Math и другие.
    /// </summary>
    [Fact]
    public void GetAllSubjectsTest()
    {
        var subjects = _fixture.GradesList
            .Select(g => g.Subject).ToList();

        Assert.Contains(subjects, s => s.Name == "Biology");
        Assert.Contains(subjects, s => s.Name == "Literature");
        Assert.Contains(subjects, s => s.Name == "Math");
        Assert.Contains(subjects, s => s.Name == "Chemistry");
        Assert.Contains(subjects, s => s.Name == "English");
        Assert.Contains(subjects, s => s.Name == "Computer Science");
        Assert.Contains(subjects, s => s.Name == "Geography");
        Assert.Contains(subjects, s => s.Name == "History");
        Assert.Contains(subjects, s => s.Name == "Physics");
        Assert.Contains(subjects, s => s.Name == "Russian");
    }

    /// <summary>
    /// Тест проверяет, что список учеников, относящихся к определенному классу (с ID 5),
    /// отсортирован по ФИО в правильном порядке.
    /// </summary>
    [Fact]
    public void GetStudentsByClassOrderedByFioTest()
    {
        var classId = 5;

        var students = _fixture.GradesList
            .Select(g => g.Student)
            .Where(s => s.Class.Id == classId)
            .OrderBy(s => s.Surname)
            .ThenBy(s => s.Name)
            .ThenBy(s => s.Patronymic)
            .ToList();

        Assert.Equal(3, students.Count);
        Assert.Equal("Mikhailov Igor Vladimirovich", $"{students[0].Surname} {students[0].Name} {students[0].Patronymic}");
        Assert.Equal("Petrov Anton Dmitrievich", $"{students[1].Surname} {students[1].Name} {students[1].Patronymic}");
        Assert.Equal("Sidorov Sergey Antonovich", $"{students[2].Surname} {students[2].Name} {students[2].Patronymic}");
    }

    /// <summary>
    /// Тест проверяет, что в списке учеников, получивших оценку на определенную дату (30 сентября 2023 года),
    /// содержится ученик с фамилией Mikhailov и именем Vladimir.
    /// </summary>
    [Fact]
    public void GetStudentsWithGradesOnSpecificDayTest()
    {
        var specificDate = new DateOnly(2023, 09, 30);

        var students = _fixture.GradesList
            .Where(g => g.Date == specificDate)
            .Select(g => g.Student)
            .Distinct()
            .ToList();

        Assert.Contains(students, s => s.Surname == "Mikhailov" && s.Name == "Vladimir");
    }

    /// <summary>
    /// Тест проверяет, что список топ-5 учеников с наивысшим средним баллом
    /// отсортирован по средней оценке и ФИО, и содержит ожидаемые значения.
    /// </summary>
    [Fact]
    public void GetTop5StudentsByAverageGradeTest()
    {
        var topStudents = _fixture.GradesList
            .GroupBy(g => g.Student)
            .Select(g => new
            {
                Student = g.Key,
                AverageGrade = g.Average(gr => (int)gr.GradeValue)
            })
            .OrderByDescending(s => s.AverageGrade)
            .ThenBy(s => s.Student.Surname) 
            .ThenBy(s => s.Student.Name)
            .ThenBy(s => s.Student.Patronymic)
            .Take(5) 
            .ToList();

        Assert.Equal(5, topStudents.Count);
        Assert.Equal("Fedorov Dmitry Sergeevich", $"{topStudents[0].Student.Surname} {topStudents[0].Student.Name} {topStudents[0].Student.Patronymic}");
        Assert.Equal("Kuznetsov Anton Antonovich", $"{topStudents[1].Student.Surname} {topStudents[1].Student.Name} {topStudents[1].Student.Patronymic}");
        Assert.Equal("Kuznetsov Semen Igorevich", $"{topStudents[2].Student.Surname} {topStudents[2].Student.Name} {topStudents[2].Student.Patronymic}");
        Assert.Equal("Mikhailov Alexey Dmitrievich", $"{topStudents[3].Student.Surname} {topStudents[3].Student.Name} {topStudents[3].Student.Patronymic}");
        Assert.Equal("Mikhailov Alexey Olegovich", $"{topStudents[4].Student.Surname} {topStudents[4].Student.Name} {topStudents[4].Student.Patronymic}");
    }

    /// <summary>
    /// Тест проверяет, что ученики с наивысшим средним баллом за определенный период
    /// (с 1 сентября 2023 года по 31 декабря 2023 года) содержат ученика с фамилией Mikhailov.
    /// </summary>
    [Fact]
    public void GetStudentsWithMaxAverageGradeForPeriodTest()
    {
        var startDate = new DateOnly(2023, 09, 01);
        var endDate = new DateOnly(2023, 10, 01);

        var studentGrades = _fixture.GradesList
            .Where(g => g.Date >= startDate && g.Date <= endDate)
            .GroupBy(g => g.Student)
            .Select(g => new
            {
                Student = g.Key,
                AverageGrade = g.Average(gr => (int)gr.GradeValue)
            })
            .ToList();

        var maxAverageGrade = studentGrades.Max(g => g.AverageGrade);

        var topStudents = studentGrades
            .Where(g => g.AverageGrade == maxAverageGrade)
            .Select(g => g.Student)
            .ToList();

        Assert.Contains(topStudents, s => s.Surname == "Mikhailov" && s.Name == "Vladimir");
    }

    /// <summary>
    /// Тест проверяет минимальный, максимальный и средний балл по каждому предмету
    /// </summary>
    [Fact]
    public void GetGradeStatisticsBySubjectTest()
    {
        var subjectStatistics = _fixture.GradesList
            .GroupBy(g => g.Subject)
            .Select(g => new
            {
                Subject = g.Key,
                MinGrade = g.Min(gr => (int)gr.GradeValue),
                MaxGrade = g.Max(gr => (int)gr.GradeValue),
                AverageGrade = g.Average(gr => (int)gr.GradeValue)
            })
            .ToList();

        Assert.Contains(subjectStatistics, s => s.Subject.IdSubject == 1 && s.MinGrade == 3 && s.MaxGrade == 5);
        Assert.Contains(subjectStatistics, s => s.Subject.IdSubject == 9 && s.AverageGrade > 3.74 && s.AverageGrade < 3.76);
    }
}
