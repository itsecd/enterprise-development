using AdmissionCommittee.Tests.Fixtures;

namespace AdmissionCommittee.Tests.Tests;

/// <summary>
/// This class contains unit tests for testing various operations related to abiturients, applications, and specialities.
/// </summary>
/// <param name="fixture">Fixture to provide data for testing.</param>
public class TestRequests(AdmissionComitteeFixture fixture) : IClassFixture<AdmissionComitteeFixture>
{
    private AdmissionComitteeFixture _fixture = fixture;

    /// <summary>
    /// Tests the selection of abiturients by city.
    /// </summary>
    [Fact]
    public void TestSelectAbiturientsByCity()
    {
        var query = _fixture.Abiturients.Where(a => a.City == "Samara").Select(a => a.Id).ToList();

        Assert.Equivalent(2, query.Count);
        Assert.Equal([1, 4], query);
    }

    /// <summary>
    /// Tests the selection of abiturients who are older than 20 years.
    /// </summary>
    [Fact]
    public void TestSelectOlderAbiturients()
    {
        var query = _fixture.Abiturients
            .Where(a => a.BirthdayDate.AddYears(20) < DateTime.Now)
            .OrderBy(a => a.Name).Select(a => a.Name).ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal( ["Anna", "Dmitry", "Mikhail", "Olga", "Pasha"], query);
    }

    /// <summary>
    /// Tests the selection of abiturients by a specific speciality.
    /// </summary>
    [Fact]
    public void TestSelectBySpeciality()
    {
        var specialityId = _fixture.Specialities.Where(sp => sp.Name == "Cyber Security").Select(sp => sp.Id).FirstOrDefault();
        var abiturientIds = _fixture.Applications.Where(ap => specialityId == ap.SpecialityId).Select(ap => ap.AbiturientId).ToList();
        var query = _fixture.Abiturients.Where(ab => abiturientIds.Contains(ab.Id)).Select
            (
                ab => new
                {
                    Abiturient = ab,
                    TotalScore = _fixture.ExamResults.Where(er => er.AbiturientId == ab.Id).Sum(er => er.Result)
                }
            ).OrderByDescending(x => x.TotalScore).Select(x => x.Abiturient.Name).ToList();

        Assert.Equivalent(2, query.Count);
        Assert.Equal(["Anna", "Ivan"], query);
    }

    /// <summary>
    /// Tests the number of abiturients who applied to specific specialities with first priority.
    /// </summary>
    [Fact]
    public void TestFirstPrioritySpecialitiesByAbiturientsAmount()
    {
        var query1 = _fixture.Applications.Where(a => a.SpecialityId == 0 && a.Priority == 1).ToList().Count();
        var query2 = _fixture.Applications.Where(a => a.SpecialityId == 1 && a.Priority == 1).ToList().Count();
        var query3 = _fixture.Applications.Where(a => a.SpecialityId == 2 && a.Priority == 1).ToList().Count();
        var query4 = _fixture.Applications.Where(a => a.SpecialityId == 3 && a.Priority == 1).ToList().Count();
        var query5 = _fixture.Applications.Where(a => a.SpecialityId == 4 && a.Priority == 1).ToList().Count();
        var query6 = _fixture.Applications.Where(a => a.SpecialityId == 5 && a.Priority == 1).ToList().Count();
        var query7 = _fixture.Applications.Where(a => a.SpecialityId == 6 && a.Priority == 1).ToList().Count();
        var query8 = _fixture.Applications.Where(a => a.SpecialityId == 7 && a.Priority == 1).ToList().Count();
        var query9 = _fixture.Applications.Where(a => a.SpecialityId == 8 && a.Priority == 1).ToList().Count();
        var query10 = _fixture.Applications.Where(a => a.SpecialityId == 9 && a.Priority == 1).ToList().Count();

        Assert.Equivalent(2, query1);
        Assert.Equivalent(1, query2);
        Assert.Equivalent(1, query3);
        Assert.Equivalent(1, query4);
        Assert.Equivalent(1, query5);
        Assert.Equivalent(1, query6);
        Assert.Equivalent(0, query7);
        Assert.Equivalent(1, query8);
        Assert.Equivalent(1, query9);
        Assert.Equivalent(1, query10);
    }

    /// <summary>
    /// Tests the selection of top 5 abiturients based on their exam scores.
    /// </summary>
    [Fact]
    public void TestTopRatedAbiturients()
    {
        var query = _fixture.Abiturients.Select
        (ab => new
        {
            Abiturient = ab,
            Score = _fixture.ExamResults.Where(er => er.AbiturientId == ab.Id).Sum(er => er.Result)
        }
        ).OrderByDescending(a => a.Score).Take(5).Select(a => a.Abiturient.Name).ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal(["Anna", "Ivan", "Natalia", "Olga", "Pasha"], query);
    }

    /// <summary>
    /// Tests the favourite specialities chosen by the top-rated abiturients based on maximum exam scores.
    /// </summary>
    [Fact]
    public void TestFavoriteSpecialitiesByopRatedAbiturients()
    {
        var maxScoreByExam = _fixture.ExamResults.GroupBy(er => er.ExamName).Select(g => new
        {
            ExamName = g.Key,
            MaxScore = g.Max(er => er.Result)
        });
        var query = maxScoreByExam.Join(_fixture.ExamResults, maxScore => maxScore.MaxScore, er => er.Result, (maxScore, er) => new
        {
            maxScore,
            er
        }).Where(joined => joined.maxScore.ExamName == joined.er.ExamName)
        .Join(_fixture.Abiturients, joined => joined.er.AbiturientId, ab => ab.Id, (joined, ab) => new
        {
            joined,
            ab
        }).Join(_fixture.Applications, abJoined => abJoined.ab.Id, ap => ap.AbiturientId, (abJoined, ap) => new
        {
            Abiturient = abJoined.ab,
            SpecialityId = ap.SpecialityId,
            ExamName = abJoined.joined.er.ExamName,
            MaxScore = abJoined.joined.er.Result,
            Priority = ap.Priority
        }).Where(q => q.Priority == 1).Select(q => q).ToList();

        Assert.Equivalent(9, query.Count);
        Assert.Equal(["Anna", "Natalia", "Anna", "Olga", "Anna", "Natalia", "Ivan", "Ivan", "Dmitry"], query.Select(q => q.Abiturient.Name).ToList());
        Assert.Equal([0, 4, 0, 1, 0, 4, 9, 9, 8], query.Select(q => q.SpecialityId).ToList());
    }
}
