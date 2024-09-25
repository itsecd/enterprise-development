using RecruitmentAgency.Domain;

namespace RecruitmentAgency.Tests;

public class RecruitmentAgencyFixture
{
    public List<Applicant> Applicants =
    [
        new() {Id = 1, FullName = "Alexey Dmitrievich Zemel", ContactInformation = "89298764563", Experience = 2.5, Education = "SSAU", Salaries = 50000 },
        new() { Id = 2, FullName = "Maria Ivanovna Petrova", ContactInformation = "89051234567", Experience = 5, Education = "MGU", Salaries = 75000 },
        new() { Id = 3, FullName = "Dmitry Sergeevich Orlov", ContactInformation = "89109876543", Experience = 3, Education = "SPbGU", Salaries = 60000 },
        new() { Id = 4, FullName = "Ekaterina Vladimirovna Smirnova", ContactInformation = "89065554433", Experience = 1.5, Education = "ITMO", Salaries = 45000 },
        new() { Id = 5, FullName = "Nikolay Viktorovich Ivanov", ContactInformation = "89261112233", Experience = 4, Education = "MIPT", Salaries = 70000 },
        new() { Id = 6, FullName = "Anna Aleksandrovna Sokolova", ContactInformation = "89092223344", Experience = 2, Education = "NSTU", Salaries = 52000 },
        new() { Id = 7, FullName = "Sergey Petrovich Kuznetsov", ContactInformation = "89371114455", Experience = 6, Education = "MEPhI", Salaries = 80000 },
        new() { Id = 8, FullName = "Olga Mikhailovna Pavlova", ContactInformation = "89284443355", Experience = 3.5, Education = "RGGU", Salaries = 62000 },
        new() { Id = 9, FullName = "Andrey Yurievich Fedorov", ContactInformation = "89174445566", Experience = 5.5, Education = "BMSTU", Salaries = 78000 },
        new() { Id = 10, FullName = "Svetlana Romanovna Belova", ContactInformation = "89096667788", Experience = 4.5, Education = "HSE", Salaries = 69000 },
        new() { Id = 11, FullName = "Ivan Nikolaevich Morozov", ContactInformation = "89172224477", Experience = 2.8, Education = "VGIK", Salaries = 53000 },

    ];

    public List<Employer> Employers =
    [
        new() {Id = 1, CompanyName = "Sberbank", ContactPersonName = "Petr Petrovich Dodonov", CompanyNumber = "89198034565"},
        new() { Id = 2, CompanyName = "Yandex", ContactPersonName = "Ivan Ivanovich Romanov", CompanyNumber = "89231112233" },
        new() { Id = 3, CompanyName = "Gazprom", ContactPersonName = "Sergey Aleksandrovich Mikhailov", CompanyNumber = "89055557766" },
        new() { Id = 4, CompanyName = "Rosneft", ContactPersonName = "Viktor Nikolaevich Kuznetsov", CompanyNumber = "89123334455" },
        new() { Id = 5, CompanyName = "Tinkoff", ContactPersonName = "Anna Vladimirovna Smirnova", CompanyNumber = "89287776655" },
        new() { Id = 6, CompanyName = "Mail.ru Group", ContactPersonName = "Olga Petrovna Fedorova", CompanyNumber = "89378889911" },
    ];

    public List<Position> Positions =
    [
        new() {Id = 1, Section = "IT", PositionName = "Software Engineer" },
        new() { Id = 2, Section = "Finance", PositionName = "Financial Analyst" },
        new() { Id = 3, Section = "Marketing", PositionName = "Marketing Specialist" },
        new() { Id = 4, Section = "HR", PositionName = "HR Manager" },
        new() { Id = 5, Section = "Sales", PositionName = "Sales Manager" },
        new() { Id = 6, Section = "IT", PositionName = "Data Scientist" },
        new() { Id = 7, Section = "Legal", PositionName = "Corporate Lawyer" },
        new() { Id = 8, Section = "IT", PositionName = "DevOps Engineer" },
        new() { Id = 9, Section = "Operations", PositionName = "Operations Manager" },
        new() { Id = 10, Section = "Customer Support", PositionName = "Customer Support Representative" },
        new() { Id = 11, Section = "IT", PositionName = "Excel" }
    ];

    public List<ApplicantApplication> ApplicantApplications;

    public List<EmployerApplication> EmployerApplications;
    public RecruitmentAgencyFixture()
    {
        ApplicantApplications =
        [
            new() { Id = 1, SubmissionDate = new DateTime(2024, 3, 2), Applicant = Applicants[1], Position = Positions[10] },
            new() { Id = 2, SubmissionDate = new DateTime(2024, 3, 10), Applicant = Applicants[2], Position = Positions[2] },
            new() { Id = 3, SubmissionDate = new DateTime(2024, 4, 5), Applicant = Applicants[3], Position = Positions[5] },
            new() { Id = 4, SubmissionDate = new DateTime(2024, 4, 18), Applicant = Applicants[4], Position = Positions[4] },
            new() { Id = 5, SubmissionDate = new DateTime(2024, 5, 1), Applicant = Applicants[5], Position = Positions[1] },
            new() { Id = 6, SubmissionDate = new DateTime(2024, 5, 15), Applicant = Applicants[6], Position = Positions[6] },
            new() { Id = 7, SubmissionDate = new DateTime(2024, 6, 3), Applicant = Applicants[7], Position = Positions[3] },
            new() { Id = 8, SubmissionDate = new DateTime(2024, 6, 25), Applicant = Applicants[8], Position = Positions[0] },
            new() { Id = 9, SubmissionDate = new DateTime(2024, 7, 8), Applicant = Applicants[9], Position = Positions[0] },
            new() { Id = 10,SubmissionDate = new DateTime(2024, 7, 20), Applicant = Applicants[10], Position = Positions[10] },
            new() { Id = 11, SubmissionDate = new DateTime(2024, 6, 3), Applicant = Applicants[7], Position = Positions[7] },
            new() { Id = 12, SubmissionDate = new DateTime(2024, 6, 3), Applicant = Applicants[3], Position = Positions[8] },
            new() { Id = 13, SubmissionDate = new DateTime(2024, 6, 3), Applicant = Applicants[2], Position = Positions[9] },
        ];
        EmployerApplications =
        [
            new() { Id = 1, SubmissionDate = new DateTime(2024, 1, 9), Employer = Employers[1], Position = Positions[2], Requirements = 2, OfferedSalary = 50000 },
            new() { Id = 1, SubmissionDate = new DateTime(2024, 1, 9), Employer = Employers[1], Position = Positions[2], Requirements = 2, OfferedSalary = 50000 },
            new() { Id = 2, SubmissionDate = new DateTime(2024, 1, 12), Employer = Employers[2], Position = Positions[3], Requirements = 3, OfferedSalary = 60000 },
            new() { Id = 3, SubmissionDate = new DateTime(2024, 1, 15), Employer = Employers[3], Position = Positions[1], Requirements = 5, OfferedSalary = 70000 },
            new() { Id = 4, SubmissionDate = new DateTime(2024, 1, 20), Employer = Employers[4], Position = Positions[4], Requirements = 1, OfferedSalary = 45000 },
            new() { Id = 11, SubmissionDate = new DateTime(2024, 3, 2), Employer = Employers[5], Position = Positions[2], Requirements = 2, OfferedSalary = 52000 },  // Подходит также заявителю
            new() { Id = 12, SubmissionDate = new DateTime(2024, 3, 5), Employer = Employers[5], Position = Positions[3], Requirements = 3, OfferedSalary = 60000 },
            new() { Id = 13, SubmissionDate = new DateTime(2024, 3, 10), Employer = Employers[4], Position = Positions[6], Requirements = 3, OfferedSalary = 64000 },  // Подходит также заявителю
            new() { Id = 14, SubmissionDate = new DateTime(2024, 3, 15), Employer = Employers[3], Position = Positions[7], Requirements = 2, OfferedSalary = 57000 },
            new() { Id = 15, SubmissionDate = new DateTime(2024, 3, 20), Employer = Employers[2], Position = Positions[8], Requirements = 1, OfferedSalary = 49000 },  // Подходит также заявителю

        ];
    }
}
