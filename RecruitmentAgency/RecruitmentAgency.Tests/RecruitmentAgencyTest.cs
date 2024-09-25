using RecruitmentAgency.Domain;

namespace RecruitmentAgency.Tests;

public class RecruitmentAgencyTest(RecruitmentAgencyFixture fixture) : IClassFixture<RecruitmentAgencyFixture>
{
    private readonly RecruitmentAgencyFixture _fixture = fixture;

    [Fact]
    public void ReturnAllApplicantsByPositionOrderedByFullName()
    {
        var positionName = "Software Engineer";
        var expectedData = new List<Applicant>
            {
                _fixture.Applicants[8],
                _fixture.Applicants[9],
            };

        var sortedApplicants = _fixture.ApplicantApplications
            .Where(a => a.Position.PositionName == positionName)
            .Select(a => a.Applicant)
            .OrderBy(a => a.FullName)
            .ToList();

        Assert.Equal(expectedData, sortedApplicants);
    }

    [Fact]
    public void ReturnAllApplicantsBySubmissionDateRange()
    {
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 4, 28);
        var expectedData = new List<Applicant>
            {
                _fixture.Applicants[1], 
                _fixture.Applicants[2],
                _fixture.Applicants[3],
                _fixture.Applicants[4]
            };

        var applicantsInRange = _fixture.ApplicantApplications
            .Where(a => a.SubmissionDate >= startDate && a.SubmissionDate <= endDate)
            .Select(a => a.Applicant)
            .ToList();

        Assert.Equal(expectedData, applicantsInRange);
    }

    [Fact]
    public void ReturnApplicantsForEmployerApplication()
    {
        var employerApplicationId = 6; 
        var expectedData = new List<Applicant>
            {
                _fixture.Applicants[3]       
            };

        var applicantsForEmployer = _fixture.ApplicantApplications
            .Where(a => a.Position.Id == employerApplicationId)
            .Select(a => a.Applicant)
            .ToList();

        Assert.Equal(expectedData, applicantsForEmployer);
    }

    [Fact]
    public void TestApplicationCountBySectionAndPositionAll()
    {
        var expectedData = new List<(string Section, string Position, int Count)>
    {
        ("IT", "Software Engineer", 2),
        ("Finance", "Financial Analyst", 1),
        ("Marketing", "Marketing Specialist", 1),
        ("HR", "HR Manager", 1),
        ("Sales", "Sales Manager", 1),
        ("IT", "Data Scientist", 1),
        ("Legal", "Corporate Lawyer", 1),
        ("IT", "DevOps Engineer", 1),
        ("Operations", "Operations Manager", 1),
        ("Customer Support", "Customer Support Representative", 1),
        ("IT", "Excel", 2)
    };

        var applicationCountBySectionAndPosition = _fixture.ApplicantApplications
            .GroupBy(app => new { app.Position.Section, app.Position.PositionName })
            .Select(group => new
            {
                Section = group.Key.Section,
                Position = group.Key.PositionName,
                Count = group.Count()
            })
            .ToList();

        var actualData = applicationCountBySectionAndPosition
            .Select(ac => (ac.Section, ac.Position, ac.Count))
            .ToList();

        Assert.Equal(
            expectedData.OrderBy(x => x.Section).ThenBy(x => x.Position),
            actualData.OrderBy(x => x.Section).ThenBy(x => x.Position)
        );
    }


    [Fact]
    public void ReturnTop5EmployersByApplicationCount()
    {
        var expectedData = new List<Employer>
            {
                _fixture.Employers[1],
                _fixture.Employers[2],
                _fixture.Employers[3],
                _fixture.Employers[4],
                _fixture.Employers[5]
            };

        var topEmployers = _fixture.EmployerApplications
            .GroupBy(e => e.Employer)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        Assert.Equal(expectedData, topEmployers);
    }

    [Fact]
    public void ReturnEmployersWithMaxSalaryApplications()
    {
        var maxSalary = _fixture.EmployerApplications.Max(e => e.OfferedSalary);
        var expectedData = new List<Employer>
            {
                _fixture.Employers[3]
            };

        var employersWithMaxSalary = _fixture.EmployerApplications
            .Where(e => e.OfferedSalary == maxSalary)
            .Select(e => e.Employer)
            .Distinct()
            .ToList();

        Assert.Equal(expectedData, employersWithMaxSalary);
    }
}


