using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Tests.Fixtures;
using System.Text.RegularExpressions;

namespace AdmissionCommittee.Tests.Tests;

/// <summary>
/// contains unit tests for testing various operations related to applicants, applications, and specialities.
/// </summary>
/// <param name="fixture">Fixture to provide data for testing</param>
public class TestRequests(AdmissionComitteeFixture fixture) : IClassFixture<AdmissionComitteeFixture>
{
    private readonly AdmissionComitteeFixture _fixture = fixture;

    /// <summary>
    /// Tests the selection of applicants by city.
    /// </summary>
    [Fact]
    public void TestApplicantsByCity()
    {
        var testCity = "Vladivostok";
        var query = _fixture.Applicants
            .Where(a => a.City == testCity)
            .Select(a => a.Id)
            .ToList();

        Assert.Equal(3, query.Count);
        Assert.Equal([2, 6, 9], query);
    }

    /// <summary>
    /// Tests the selection of applicants who are 20+ years old.
    /// </summary>
    [Fact]
    public void TestOlderApplicants()
    {
        var testYear = 20;
        var testDateTime = "10.10.2024";
        var query = _fixture.Applicants
            .Where(a => a.BirthdayDate.AddYears(testYear) < DateTime.Parse(testDateTime))
            .OrderBy(a => a.FullName)
            .Select(a => a.Id)
            .ToList();

        Assert.Equal(6, query.Count);
        Assert.Equal([1, 5, 7, 3, 8, 4], query);
    }

    /// <summary>
    /// Tests the selection of applicants by a specific speciality.
    /// </summary>
    [Fact]
    public void TestSelectBySpeciality()
    {
        var testSpecialitieName = "Psychology";

        var query = _fixture.Specialities
            .Join(
                _fixture.Applications,
                Speciality => Speciality.Id,
                Applications => Applications.Id,
                (Speciality, Applications) => new { Speciality, Applications }
            )
            .Join(
            _fixture.Applicants,
            specialitiesWithApplicants => specialitiesWithApplicants.Applications.ApplicantId,
            Applicant => Applicant.Id,
            (specialitiesWithApplicants, Applicant) => new { specialitiesWithApplicants, Applicant }
            )
            .Where(SpecAppApplicant => SpecAppApplicant.specialitiesWithApplicants.Speciality.Name == testSpecialitieName)
            .Select(SpecAppApplicant => SpecAppApplicant.specialitiesWithApplicants.Speciality.Id)
            .ToList();

        Assert.Equal(1, query.Count);
        Assert.Equal([1], [1]);
    }

    /// <summary>
    /// Tests the number of Applicants who applied to specific specialities with first priority.
    /// </summary>
    [Fact]
    public void TestFirstPrioritySpecialitiesByApplicantsAmount()
    { 
        var testPriorityValue = 1;
        
        var query = _fixture.Applications
            .Where(Application => Application.Priority == testPriorityValue)
            .GroupBy(Application => Application.SpecialityId)
            .Select(Group => new
            {
                Key = Group.Key,
                Count = Group.Count()
            })
            .Select(result => result.Count)
            .ToList();

        Assert.Equal([3,1,2,1], query);
    }

    /// <summary>
    /// Tests the selection of top 5 Applicants based on their exam scores.
    /// </summary>
    [Fact]
    public void TestTopRatedApplicants()
    {
        var query = _fixture.Applicants
            .Select
            (Applicant => new
            {
                Applicant = Applicant,
                Score = _fixture.ExamResults
                .Where(ExamRes => ExamRes.ApplicantId == Applicant.Id)
                .Sum(ExamRes => ExamRes.Result)
            }
            ).OrderByDescending(a => a.Score)
            .Take(5)
            .Select(a => a.Applicant.Id)
            .ToList();

        Assert.Equal(5, query.Count);
        Assert.Equal([3,9,7,5,1], query);
    }

    /// <summary>
    /// Tests the favourite specialities chosen by the top-rated applicants based on maximum exam scores.
    /// </summary>
    [Fact]
    public void TestFavoriteSpecialitiesByopRatedApplicants()
    {
        var maxScoreByExam = _fixture.ExamResults
            .GroupBy(ExamRes => ExamRes.ExamName)
            .Select(Group => new
            {
                ExamName = Group.Key,
                MaxScore = Group.Max(ExamRes => ExamRes.Result)
            });

        var query = maxScoreByExam
            .Join(
            _fixture.ExamResults,
            maxScore => maxScore.MaxScore,
            ExamRes => ExamRes.Result,
            (maxScore, ExamRes) => new { maxScore, ExamRes }
            )
            .Where(joined => joined.maxScore.ExamName == joined.ExamRes.ExamName)
            .Join(
            _fixture.Applicants,
            MaxScore => MaxScore.ExamRes.ApplicantId,
            Applicant => Applicant.Id,
            (MaxScore, Applicant) => new { MaxScore, Applicant }
            )
            .Join(
            _fixture.Applications,
            MaxScore => MaxScore.Applicant.Id,
            Application => Application.ApplicantId,
            (MaxScore, Application) => new
            {
                Applicant = MaxScore.Applicant,
                Application.SpecialityId,
                MaxScore.MaxScore.ExamRes.ExamName,
                MaxScore = MaxScore.MaxScore.ExamRes.Result,
                Application.Priority
            })
            .Where(Speciality => Speciality.Priority == 1)
            .Select(Speciality => Speciality)
            .ToList();

        Assert.Equal(4, query.Count);
        Assert.Equal([3,3,3,4],
                     query.Select(q => q.Applicant.Id).ToList());

        Assert.Equal([0, 0, 0, 8], query.Select(q => q.SpecialityId).ToList());
    }
}
