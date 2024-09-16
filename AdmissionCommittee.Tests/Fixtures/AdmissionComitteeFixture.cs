using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Tests.Fixtures;

public class AdmissionComitteeFixture
{
    public List<Application> Applications = new()
{
    new Application { Id = 0, AbiturientId = 0, SpecialityId = 0, Priority = 1 },
    new Application { Id = 1, AbiturientId = 0, SpecialityId = 1, Priority = 2 },
    new Application { Id = 2, AbiturientId = 0, SpecialityId = 2, Priority = 3 },

    new Application { Id = 3, AbiturientId = 1, SpecialityId = 3, Priority = 1 },
    new Application { Id = 4, AbiturientId = 1, SpecialityId = 4, Priority = 2 },

    new Application { Id = 5, AbiturientId = 2, SpecialityId = 5, Priority = 1 },

    new Application { Id = 6, AbiturientId = 3, SpecialityId = 0, Priority = 1 },
    new Application { Id = 7, AbiturientId = 3, SpecialityId = 6, Priority = 2 },
    new Application { Id = 8, AbiturientId = 3, SpecialityId = 7, Priority = 3 },

    new Application { Id = 9, AbiturientId = 4, SpecialityId = 8, Priority = 1 },
    new Application { Id = 10, AbiturientId = 4, SpecialityId = 9, Priority = 2 },

    new Application { Id = 11, AbiturientId = 5, SpecialityId = 1, Priority = 1 },
    new Application { Id = 12, AbiturientId = 5, SpecialityId = 3, Priority = 2 },

    new Application { Id = 13, AbiturientId = 6, SpecialityId = 2, Priority = 1 },

    new Application { Id = 14, AbiturientId = 7, SpecialityId = 4, Priority = 1 },
    new Application { Id = 15, AbiturientId = 7, SpecialityId = 5, Priority = 2 },
    new Application { Id = 16, AbiturientId = 7, SpecialityId = 6, Priority = 3 },

    new Application { Id = 17, AbiturientId = 8, SpecialityId = 7, Priority = 1 },
    new Application { Id = 18, AbiturientId = 8, SpecialityId = 8, Priority = 2 },

    new Application { Id = 19, AbiturientId = 9, SpecialityId = 9, Priority = 1 },
};


    public List<Abiturient> Abiturients = new()
{
    new Abiturient { Id = 0, BirthdayDate = new DateTime(2005, 5, 18), City = "Moscow", Country = "Russia", Name = "Ivan"},
    new Abiturient { Id = 1, BirthdayDate = new DateTime(2003, 6, 11), City = "Samara", Country = "Russia", Name = "Pasha"},
    new Abiturient { Id = 2, BirthdayDate = new DateTime(2004, 11, 8), City = "Rostov", Country = "Russia", Name = "Roma"},
    new Abiturient { Id = 3, BirthdayDate = new DateTime(2003, 1, 13), City = "Saint Petersburg", Country = "Russia", Name = "Anna"},
    new Abiturient { Id = 4, BirthdayDate = new DateTime(2004, 5, 22), City = "Samara", Country = "Russia", Name = "Dmitry"},
    new Abiturient { Id = 5, BirthdayDate = new DateTime(2003, 2, 5), City = "Novosibirsk", Country = "Russia", Name = "Olga"},
    new Abiturient { Id = 6, BirthdayDate = new DateTime(2005, 1, 28), City = "Yekaterinburg", Country = "Russia", Name = "Sergey"},
    new Abiturient { Id = 7, BirthdayDate = new DateTime(2005, 8, 1), City = "Nizhny Novgorod", Country = "Russia", Name = "Natalia"},
    new Abiturient { Id = 8, BirthdayDate = new DateTime(2003, 9, 7), City = "Chelyabinsk", Country = "Russia", Name = "Mikhail"},
    new Abiturient { Id = 9, BirthdayDate = new DateTime(2004, 11, 16), City = "Omsk", Country = "Russia", Name = "Ivan"}
};

    public List<ExamResult> ExamResults = new()
{
    new ExamResult { Id = 0, AbiturientId = 0, ExamName = "Mathematics", Result = 82 },
    new ExamResult { Id = 1, AbiturientId = 0, ExamName = "Physics", Result = 75 },
    new ExamResult { Id = 2, AbiturientId = 0, ExamName = "Computer Science", Result = 90 },

    new ExamResult { Id = 3, AbiturientId = 1, ExamName = "Mathematics", Result = 88 },
    new ExamResult { Id = 4, AbiturientId = 1, ExamName = "Chemistry", Result = 79 },
    new ExamResult { Id = 5, AbiturientId = 1, ExamName = "English", Result = 85 },

    new ExamResult { Id = 6, AbiturientId = 2, ExamName = "Biology", Result = 70 },
    new ExamResult { Id = 7, AbiturientId = 2, ExamName = "Physics", Result = 68 },
    new ExamResult { Id = 8, AbiturientId = 2, ExamName = "History", Result = 72 },

    new ExamResult { Id = 9, AbiturientId = 3, ExamName = "Mathematics", Result = 95 },
    new ExamResult { Id = 10, AbiturientId = 3, ExamName = "English", Result = 89 },
    new ExamResult { Id = 11, AbiturientId = 3, ExamName = "Computer Science", Result = 92 },

    new ExamResult { Id = 12, AbiturientId = 4, ExamName = "History", Result = 78 },
    new ExamResult { Id = 13, AbiturientId = 4, ExamName = "Geography", Result = 82 },
    new ExamResult { Id = 14, AbiturientId = 4, ExamName = "Literature", Result = 75 },

    new ExamResult { Id = 15, AbiturientId = 5, ExamName = "Physics", Result = 80 },
    new ExamResult { Id = 16, AbiturientId = 5, ExamName = "Mathematics", Result = 85 },
    new ExamResult { Id = 17, AbiturientId = 5, ExamName = "Chemistry", Result = 88 },

    new ExamResult { Id = 18, AbiturientId = 6, ExamName = "English", Result = 65 },
    new ExamResult { Id = 19, AbiturientId = 6, ExamName = "Mathematics", Result = 70 },
    new ExamResult { Id = 20, AbiturientId = 6, ExamName = "Physics", Result = 75 },

    new ExamResult { Id = 21, AbiturientId = 7, ExamName = "Biology", Result = 90 },
    new ExamResult { Id = 22, AbiturientId = 7, ExamName = "Chemistry", Result = 85 },
    new ExamResult { Id = 23, AbiturientId = 7, ExamName = "Physics", Result = 88 },

    new ExamResult { Id = 24, AbiturientId = 8, ExamName = "Mathematics", Result = 60 },
    new ExamResult { Id = 25, AbiturientId = 8, ExamName = "Computer Science", Result = 65 },
    new ExamResult { Id = 26, AbiturientId = 8, ExamName = "English", Result = 78 },

    new ExamResult { Id = 27, AbiturientId = 9, ExamName = "History", Result = 92 },
    new ExamResult { Id = 28, AbiturientId = 9, ExamName = "Geography", Result = 88 },
    new ExamResult { Id = 29, AbiturientId = 9, ExamName = "Physics", Result = 85 },
};



    public List<Speciality> Specialities = new()
{
    new Speciality { Id = 0, Number = "105003D", Name = "Cyber Security", Facility = "Informatics"},
    new Speciality { Id = 1, Number = "205004A", Name = "Mechanical Engineering", Facility = "Engineering"},
    new Speciality { Id = 2, Number = "305005B", Name = "Business Administration", Facility = "Business"},
    new Speciality { Id = 3, Number = "405006C", Name = "Psychology", Facility = "Social Sciences"},
    new Speciality { Id = 4, Number = "505007D", Name = "Biology", Facility = "Natural Sciences"},
    new Speciality { Id = 5, Number = "605008E", Name = "Architecture", Facility = "Arts and Architecture"},
    new Speciality { Id = 6, Number = "705009F", Name = "Law", Facility = "Law School"},
    new Speciality { Id = 7, Number = "805010G", Name = "Medicine", Facility = "Medical School"},
    new Speciality { Id = 8, Number = "905011H", Name = "Environmental Studies", Facility = "Environmental Sciences"},
    new Speciality { Id = 9, Number = "1005012I", Name = "Philosophy", Facility = "Humanities"}
};

}
