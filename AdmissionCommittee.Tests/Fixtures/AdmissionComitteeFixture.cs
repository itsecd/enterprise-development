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
        new() { Id = 0, BirthdayDate = new DateTime(2005, 1, 18), City = "Samara", Country = "Russia", FullName = "Vladimir Vladimirovich" },
        new() { Id = 1, BirthdayDate = new DateTime(2002, 2, 9), City = "Samara", Country = "Russia", FullName = "Andrew Viktorovich" },
        new() { Id = 2, BirthdayDate = new DateTime(2005, 7, 8), City = "Vladivostok", Country = "Russia", FullName = "Vitaliy Vitalivich" },
        new() { Id = 3, BirthdayDate = new DateTime(2004, 1, 13), City = "Samara", Country = "Russia", FullName = "Michail Michailovich" },
        new() { Id = 4, BirthdayDate = new DateTime(2004, 6, 2), City = "Saints-Petersburg", Country = "Russia", FullName = "Veronika Igorevna" },
        new() { Id = 5, BirthdayDate = new DateTime(2004, 2, 12), City = "Samara", Country = "Russia", FullName = "Ivan Ivanov" },
        new() { Id = 6, BirthdayDate = new DateTime(2005, 2, 22), City = "Vladivostok", Country = "Russia", FullName = "Danila Danilovich" },
        new() { Id = 7, BirthdayDate = new DateTime(2001, 2, 2), City = "Samara", Country = "Russia", FullName = "Maria Olegovna" },
        new() { Id = 8, BirthdayDate = new DateTime(2002, 1, 4), City = "Moscow", Country = "Russia", FullName = "Sergey Sergeevich" },
        new() { Id = 9, BirthdayDate = new DateTime(2004, 12, 1), City = "Vladivostok", Country = "Russia", FullName = "Vladimir Vladimirov" }
    ];

    public List<ExamResult> ExamResults =
    [
        new() { Id = 0, ApplicantId = 0, ExamName = "Math", Result = 82 },
        new() { Id = 1, ApplicantId = 0, ExamName = "Phisics", Result = 75 },
        new() { Id = 2, ApplicantId = 0, ExamName = "English", Result = 90 },
        new() { Id = 3, ApplicantId = 1, ExamName = "Math", Result = 88 },
        new() { Id = 4, ApplicantId = 1, ExamName = "Informatics", Result = 79 },
        new() { Id = 5, ApplicantId = 1, ExamName = "Social Science", Result = 85 },
        new() { Id = 6, ApplicantId = 2, ExamName = "Literature", Result = 70 },
        new() { Id = 7, ApplicantId = 2, ExamName = "Phisics", Result = 68 },
        new() { Id = 8, ApplicantId = 2, ExamName = "Russian Language", Result = 72 },
        new() { Id = 9, ApplicantId = 3, ExamName = "Math", Result = 95 },
        new() { Id = 10, ApplicantId = 3, ExamName = "Social Science", Result = 89 },
        new() { Id = 11, ApplicantId = 3, ExamName = "English", Result = 92 },
        new() { Id = 12, ApplicantId = 4, ExamName = "Russian Language", Result = 78 },
        new() { Id = 13, ApplicantId = 4, ExamName = "Chemistry", Result = 82 },
        new() { Id = 14, ApplicantId = 4, ExamName = "Biology", Result = 75 },
        new() { Id = 15, ApplicantId = 5, ExamName = "Phisics", Result = 80 },
        new() { Id = 16, ApplicantId = 5, ExamName = "Math", Result = 85 },
        new() { Id = 17, ApplicantId = 5, ExamName = "Informatics", Result = 88 },
        new() { Id = 18, ApplicantId = 6, ExamName = "Social Science", Result = 65 },
        new() { Id = 19, ApplicantId = 6, ExamName = "Math", Result = 70 },
        new() { Id = 20, ApplicantId = 6, ExamName = "Phisics", Result = 75 },
        new() { Id = 21, ApplicantId = 7, ExamName = "Literature", Result = 90 },
        new() { Id = 22, ApplicantId = 7, ExamName = "Informatics", Result = 85 },
        new() { Id = 23, ApplicantId = 7, ExamName = "Phisics", Result = 88 },
        new() { Id = 24, ApplicantId = 8, ExamName = "Math", Result = 60 },
        new() { Id = 25, ApplicantId = 8, ExamName = "English", Result = 65 },
        new() { Id = 26, ApplicantId = 8, ExamName = "Social Science", Result = 78 },
        new() { Id = 27, ApplicantId = 9, ExamName = "Russian Language", Result = 92 },
        new() { Id = 28, ApplicantId = 9, ExamName = "Chemistry", Result = 88 },
        new() { Id = 29, ApplicantId = 9, ExamName = "Phisics", Result = 85 }
    ];

    public List<Speciality> Specialities =
    [
        new() { Id = 0, Number = "105003D", Name = "Cyber Security", Faculity = "Informatics" },
        new() { Id = 1, Number = "205004A", Name = "Mechanical Engineering", Faculity = "Engineering" },
        new() { Id = 2, Number = "305005B", Name = "Business Administration", Faculity = "Business" },
        new() { Id = 3, Number = "405006C", Name = "Psychology", Faculity = "Social Sciences" },
        new() { Id = 4, Number = "505007D", Name = "Literature", Faculity = "Natural Sciences" },
        new() { Id = 5, Number = "605008E", Name = "Architecture", Faculity = "Arts and Architecture" },
        new() { Id = 6, Number = "705009F", Name = "Law", Faculity = "Law School" },
        new() { Id = 7, Number = "805010G", Name = "Medicine", Faculity = "Medical School" },
        new() { Id = 8, Number = "905011H", Name = "Environmental Studies", Faculity = "Environmental Sciences" },
        new() { Id = 9, Number = "1005012I", Name = "Philosophy", Faculity = "Humanities" }
    ];
}
