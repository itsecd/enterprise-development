using AdmissionCommittee.Tests.Fixtures;

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
        var testDateTime = new DateTime(2024, 10, 10);
        var query = _fixture.Applicants
            .Where(a => a.BirthdayDate.AddYears(testYear) < testDateTime)
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
        var testSpecialitieName = "Philosophy";


        var specialitiesApllicationApplicants = from specialities in _fixture.Specialities
                                                join applicantion in _fixture.Applications on specialities.Id equals applicantion.SpecialityId
                                                join applicants in _fixture.Applicants on applicantion.ApplicantId equals applicants.Id
                                                select applicants;



        var query = specialitiesApllicationApplicants.Select(
                 applicant => new
                 {
                     applicant,
                     TotalScore = _fixture.ExamResults
                        .Where(examResult => examResult.ApplicantId == applicant.Id)
                        .Sum(examResult => examResult.Result)
                 }
             ).OrderByDescending(x => x.TotalScore)
             .Select(x => x.applicant.FullName)
             .Distinct()
             .ToList();

        Assert.Equal(["Michail Michailovich", "Andrew Viktorovich", "Vladimir Vladimirovich", "Veronika Igorevna", "Vitaliy Vitalivich"], query);
    }

    /// <summary>
    /// Tests the number of Applicants who applied to specific specialities with first priority.
    /// </summary>
    [Fact]
    public void TestFirstPrioritySpecialitiesByApplicantsAmount()
    {
        var testPriorityValue = 1;

        var query = _fixture.Applications
            .Where(application => application.Priority == testPriorityValue)
            .GroupBy(application => application.SpecialityId)
            .Select(Group => new
            {
                Group.Key,
                Count = Group.Count()
            })
            .Select(result => result.Count)
            .ToList();

        Assert.Equal([3, 1, 2, 1], query);
    }

    /// <summary>
    /// Tests the selection of top 5 Applicants based on their exam scores.
    /// </summary>
    [Fact]
    public void TestTopRatedApplicants()
    {
        var query = _fixture.Applicants
            .Select
            (applicant => new
            {
                applicant,
                score = _fixture.ExamResults
                .Where(examRes => examRes.ApplicantId == applicant.Id)
                .Sum(examRes => examRes.Result)
            }
            ).OrderByDescending(a => a.score)
            .Take(5)
            .Select(a => a.applicant.Id)
            .ToList();

        Assert.Equal(5, query.Count);
        Assert.Equal([3, 9, 7, 5, 1], query);
    }

    /// <summary>
    /// Tests the favourite specialities chosen by the top-rated applicants based on maximum exam scores.
    /// </summary>
    [Fact]
    public void TestFavoriteSpecialitiesByopRatedApplicants()
    {
        var maxScoreByExam = _fixture.ExamResults
            .GroupBy(examRes => examRes.ExamName)
            .Select(Group => new
            {
                examName = Group.Key,
                maxScore = Group.Max(examRes => examRes.Result)
            }
            );

        var query = maxScoreByExam
            .Join(
                _fixture.ExamResults,
                maxScore => maxScore.maxScore,
                examRes => examRes.Result,
                (maxScore, examRes) => new
                {
                    maxScore,
                    examRes
                }
            )
            .Where(joined => joined.maxScore.examName == joined.examRes.ExamName)
            .Join(
                _fixture.Applicants,
                maxScore => maxScore.examRes.ApplicantId,
                applicant => applicant.Id,
                (maxScore, applicant) => new
                {
                    maxScore = maxScore,
                    applicant = applicant
                }
            )
            .Join(
                _fixture.Applications,
                maxScore => maxScore.applicant.Id,
                application => application.ApplicantId,
                (maxScore, application) => new
                {
                    Applicant = maxScore.applicant,
                    application.SpecialityId,
                    maxScore.maxScore.examRes.ExamName,
                    maxScore = maxScore.maxScore.examRes.Result,
                    application.Priority
                }
            )
            .Where(speciality => speciality.Priority == 1)
            .Select(speciality => speciality)
            .ToList();

        Assert.Equal(4, query.Count);
        Assert.Equal([3, 3, 3, 4],
                     query.Select(q => q.Applicant.Id).ToList());

        Assert.Equal([0, 0, 0, 8], query.Select(q => q.SpecialityId).ToList());
    }
}
