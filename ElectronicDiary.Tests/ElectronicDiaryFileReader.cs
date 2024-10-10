using ElectronicDiary.Domain;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace ElectronicDiary.Tests;

static class ElectronicDiaryFileReader
{
    /// <summary>
    /// Чтение данных из csv-файла с последующей записью в объекты классов
    /// </summary>
    public static List<Grade> ReadGrades(string fileName)
    {
        using var streamReader = new StreamReader(fileName);
        var grades = new List<Grade>();
        var students = new Dictionary<int, Student>();
        var subjects = new Dictionary<int, Subject>();
        var classes = new Dictionary<int, Class>();

        while (!streamReader.EndOfStream)
        {
            var gradesLine = streamReader.ReadLine();
            if (gradesLine == null || !gradesLine.Contains('"')) continue;
            gradesLine = gradesLine.Trim('"');
            var tokens = gradesLine.Split(',');

            var classKey = int.Parse(tokens[9]);
            if (!classes.TryGetValue(classKey, out var studyClass))
            {
                studyClass = new Class
                {
                    Id = classKey,
                    Number = int.Parse(tokens[10]),
                    Letters = tokens[11]
                };
                classes[classKey] = studyClass;
            }
            
            int studentKey = int.Parse(tokens[0]);
            if (!students.TryGetValue(studentKey, out var student))
            {
                student = new Student
                {
                    IdStudent = studentKey,
                    Surname = tokens[1],
                    Name = tokens[2],
                    Patronymic = tokens[3],
                    Birthday = DateOnly.ParseExact(tokens[5], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Class = studyClass,
                    Passport = tokens[4]
                };
                students[studentKey] = student;
            }

            var subjectKey = int.Parse(tokens[6]);
            if (!subjects.TryGetValue(subjectKey, out var subject))
            {
                subject = new Subject
                {
                    IdSubject = subjectKey,
                    Name = tokens[7],
                    StudyYear = tokens[8]
                };
                subjects[subjectKey] = subject;
            }

            var grade = new Grade
            {
                Id = int.Parse(tokens[14]),
                Student = student,
                Subject = subject,
                GradeValue = (GradeTypes)int.Parse(tokens[12]),
                Date = DateOnly.ParseExact(tokens[13], "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };

            grades.Add(grade);
        }
        return grades;
    }

}
