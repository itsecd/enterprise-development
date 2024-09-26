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
        var employerApplicationId = 4;
        var expectedData = new List<Applicant>
        {
            _fixture.Applicants[5] 
        };

        var applicantsForEmployer = _fixture.ApplicantApplications
            .Join(_fixture.EmployerApplications,
                  applicantApp => applicantApp.Position,
                  employerApp => employerApp.Position,
                  (applicantApp, employerApp) => new { applicantApp, employerApp })
            .Where(a => a.employerApp.Id == employerApplicationId) 
            .Where(a => a.applicantApp.Applicant.Salaries <= a.employerApp.OfferedSalary) 
            .Select(a => a.applicantApp.Applicant)
            .ToList();

        Assert.Equal(expectedData, applicantsForEmployer);
    }

    [Fact]
    public void TestApplicationCountBySectionAndPositionAll()
    {
        var expectedData = new[]
        {
            new {Section = "IT", PositionName = "Software Engineer", Count = 3},
            new {Section = "Finance", PositionName = "Financial Analyst", Count = 2},
            new {Section = "Marketing", PositionName = "Marketing Specialist", Count = 4},
            new {Section = "HR", PositionName = "HR Manager", Count = 3},
            new {Section = "Sales", PositionName = "Sales Manager", Count = 2},
            new {Section = "IT", PositionName = "Data Scientist", Count = 1},
            new {Section = "Legal", PositionName = "Corporate Lawyer", Count = 2},
            new {Section = "IT", PositionName = "DevOps Engineer", Count = 2},
            new {Section = "Operations", PositionName = "Operations Manager", Count = 2},
            new {Section = "Customer Support", PositionName = "Customer Support Representative", Count = 1},
            new {Section = "IT", PositionName = "Excel", Count = 2},
        };

        var applicationCountBySectionAndPosition = _fixture.Positions
            .GroupJoin(_fixture.ApplicantApplications,
                position => position.Id,
                app => app.Position.Id,
                (position, apps) => new { Position = position, ApplicantCount = apps.Count() })
            .GroupJoin(_fixture.EmployerApplications,
                positionWithApplicant => positionWithApplicant.Position.Id,
                employerApp => employerApp.Position.Id,
                (positionWithApplicant, employerApps) => new
                {
                    positionWithApplicant.Position.Section,
                    positionWithApplicant.Position.PositionName,
                    Count = positionWithApplicant.ApplicantCount + employerApps.Count()
                })
            .ToList();

        Assert.Equal(expectedData.OrderBy(x => x.Section).ThenBy(x => x.PositionName),
            applicationCountBySectionAndPosition
                .OrderBy(x => x.Section)
                .ThenBy(x => x.PositionName)
                .Select(x => new { x.Section, x.PositionName, x.Count })
        );
    }

    [Fact]
    public void ReturnTop5EmployersByApplicationCount()
    {
        var expectedData = new List<(Employer Employer, int ApplicationCount)>
        {
            (_fixture.Employers[1], 3),
            (_fixture.Employers[2], 2),
            (_fixture.Employers[3], 2),
            (_fixture.Employers[4], 2),
            (_fixture.Employers[5], 2)
        };

        var topEmployers = _fixture.EmployerApplications
            .GroupBy(e => e.Employer)
            .Select(g => new { Employer = g.Key, ApplicationCount = g.Count() })
            .OrderByDescending(g => g.ApplicationCount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedData.OrderBy(e => e.Employer.Id).Select(e => new { e.Employer, e.ApplicationCount }),
            topEmployers.OrderBy(e => e.Employer.Id).Select(e => new { e.Employer, e.ApplicationCount })
        );
    }

    [Fact]
    public void ReturnEmployersWithMaxSalaryApplications()
    {
        var maxSalary = _fixture.EmployerApplications.Max(e => e.OfferedSalary);
        var expectedData = new List<Employer>
        {
            _fixture.Employers[4]
        };

        var employersWithMaxSalary = _fixture.EmployerApplications
            .Where(e => e.OfferedSalary == maxSalary)
            .Select(e => e.Employer)
            .Distinct()
            .ToList();

        Assert.Equal(expectedData, employersWithMaxSalary);
    }
}


