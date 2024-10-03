using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Tests.Fixtures;

public class AdmissionComitteeFixture
{
    public List<Application> Applications =
    [
        new() { Id = 0, ApplicantId = 0, SpecialityId = 0, Priority = 1 },
        new() { Id = 1, ApplicantId = 0, SpecialityId = 0, Priority = 1 },
        new() { Id = 2, ApplicantId = 1, SpecialityId = 2, Priority = 3 },
        new() { Id = 3, ApplicantId = 1, SpecialityId = 3, Priority = 1 },
        new() { Id = 4, ApplicantId = 1, SpecialityId = 4, Priority = 2 },
        new() { Id = 5, ApplicantId = 2, SpecialityId = 5, Priority = 1 },
        new() { Id = 6, ApplicantId = 2, SpecialityId = 5, Priority = 1 },
        new() { Id = 7, ApplicantId = 3, SpecialityId = 0, Priority = 1 },
        new() { Id = 8, ApplicantId = 3, SpecialityId = 7, Priority = 3 },
        new() { Id = 9, ApplicantId = 4, SpecialityId = 8, Priority = 1 },
        new() { Id = 10, ApplicantId = 4, SpecialityId = 9, Priority = 2 },
    ];

    public List<Applicant> Applicants =
    [
        new() { Id = 0, BirthdayDate = new DateTime(2005, 1, 18), City = "Самара", Country = "Россия", Name = "Владимир" },
        new() { Id = 1, BirthdayDate = new DateTime(2002, 2, 9), City = "Самара", Country = "Россия", Name = "Андрей" },
        new() { Id = 2, BirthdayDate = new DateTime(2005, 7, 8), City = "Владивосток", Country = "Россия", Name = "Виталик" },
        new() { Id = 3, BirthdayDate = new DateTime(2004, 1, 13), City = "Самара", Country = "Россия", Name = "Миша" },
        new() { Id = 4, BirthdayDate = new DateTime(2004, 6, 2), City = "Екатеринбург", Country = "Россия", Name = "Вероника" },
        new() { Id = 5, BirthdayDate = new DateTime(2004, 2, 12), City = "Самара", Country = "Россия", Name = "Иван" },
        new() { Id = 6, BirthdayDate = new DateTime(2005, 2, 22), City = "Владивосток", Country = "Россия", Name = "Данила" },
        new() { Id = 7, BirthdayDate = new DateTime(2001, 2, 2), City = "Самара", Country = "Россия", Name = "Мария" },
        new() { Id = 8, BirthdayDate = new DateTime(2002, 1, 4), City = "Москва", Country = "Россия", Name = "Сергей" },
        new() { Id = 9, BirthdayDate = new DateTime(2004, 12, 1), City = "Владивосток", Country = "Россия", Name = "Владимир" }
    ];

    public List<ExamResult> ExamResults =
    [
        new() { Id = 0, ApplicantId = 0, ExamName = "Математика", Result = 82 },
        new() { Id = 1, ApplicantId = 0, ExamName = "Физика", Result = 75 },
        new() { Id = 2, ApplicantId = 0, ExamName = "Иностранный язык", Result = 90 },
        new() { Id = 3, ApplicantId = 1, ExamName = "Математика", Result = 88 },
        new() { Id = 4, ApplicantId = 1, ExamName = "Информатика", Result = 79 },
        new() { Id = 5, ApplicantId = 1, ExamName = "Обществознание", Result = 85 },
        new() { Id = 6, ApplicantId = 2, ExamName = "Литература", Result = 70 },
        new() { Id = 7, ApplicantId = 2, ExamName = "Физика", Result = 68 },
        new() { Id = 8, ApplicantId = 2, ExamName = "Русский язык", Result = 72 },
        new() { Id = 9, ApplicantId = 3, ExamName = "Математика", Result = 95 },
        new() { Id = 10, ApplicantId = 3, ExamName = "Обществознание", Result = 89 },
        new() { Id = 11, ApplicantId = 3, ExamName = "Иностранный язык", Result = 92 },
        new() { Id = 12, ApplicantId = 4, ExamName = "Русский язык", Result = 78 },
        new() { Id = 13, ApplicantId = 4, ExamName = "Химия", Result = 82 },
        new() { Id = 14, ApplicantId = 4, ExamName = "Биология", Result = 75 },
        new() { Id = 15, ApplicantId = 5, ExamName = "Физика", Result = 80 },
        new() { Id = 16, ApplicantId = 5, ExamName = "Математика", Result = 85 },
        new() { Id = 17, ApplicantId = 5, ExamName = "Информатика", Result = 88 },
        new() { Id = 18, ApplicantId = 6, ExamName = "Обществознание", Result = 65 },
        new() { Id = 19, ApplicantId = 6, ExamName = "Математика", Result = 70 },
        new() { Id = 20, ApplicantId = 6, ExamName = "Физика", Result = 75 },
        new() { Id = 21, ApplicantId = 7, ExamName = "Литература", Result = 90 },
        new() { Id = 22, ApplicantId = 7, ExamName = "Информатика", Result = 85 },
        new() { Id = 23, ApplicantId = 7, ExamName = "Физика", Result = 88 },
        new() { Id = 24, ApplicantId = 8, ExamName = "Математика", Result = 60 },
        new() { Id = 25, ApplicantId = 8, ExamName = "Иностранный язык", Result = 65 },
        new() { Id = 26, ApplicantId = 8, ExamName = "Обществознание", Result = 78 },
        new() { Id = 27, ApplicantId = 9, ExamName = "Русский язык", Result = 92 },
        new() { Id = 28, ApplicantId = 9, ExamName = "Химия", Result = 88 },
        new() { Id = 29, ApplicantId = 9, ExamName = "Физика", Result = 85 }
    ];

    public List<Speciality> Specialities =
    [
        new() { Id = 0, Number = "105003D", Name = "Cyber Security", Facility = "Informatics" },
        new() { Id = 1, Number = "205004A", Name = "Mechanical Engineering", Facility = "Engineering" },
        new() { Id = 2, Number = "305005B", Name = "Business Administration", Facility = "Business" },
        new() { Id = 3, Number = "405006C", Name = "Psychology", Facility = "Social Sciences" },
        new() { Id = 4, Number = "505007D", Name = "Литература", Facility = "Natural Sciences" },
        new() { Id = 5, Number = "605008E", Name = "Architecture", Facility = "Arts and Architecture" },
        new() { Id = 6, Number = "705009F", Name = "Law", Facility = "Law School" },
        new() { Id = 7, Number = "805010G", Name = "Medicine", Facility = "Medical School" },
        new() { Id = 8, Number = "905011H", Name = "Environmental Studies", Facility = "Environmental Sciences" },
        new() { Id = 9, Number = "1005012I", Name = "Philosophy", Facility = "Humanities" }
    ];
}
