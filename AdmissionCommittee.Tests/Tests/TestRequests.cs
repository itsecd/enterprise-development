using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Tests.Fixtures;

namespace AdmissionCommittee.Tests.Tests;

public class TestRequests (AdmissionComitteeFixture fixture) : IClassFixture<AdmissionComitteeFixture>
{
    private AdmissionComitteeFixture _fixture = fixture;

    [Fact]
    public void TestSelectAbiturientsByCity()
    {
        var query = _fixture.Abiturients.Where(a => a.City == "Samara").Select(a => a.Id).ToList();

        Assert.Equivalent(2, query.Count());
        Assert.Equal(new List<int> { 1, 4 }, query);
    }

    [Fact]
    public void TestSelectOlderAbiturients()
    {
        var query = _fixture.Abiturients.Where(a => a.BirthdayDate.AddYears(20) < DateTime.Now).Select(a => a.Id).ToList();

        Assert.Equivalent(5, query.Count());
        Assert.Equal(new List<int> { 1, 3, 4, 5, 8}, query);
    }

    [Fact]
    public void TestSelectBySpeciality()
    {
        var specialityId = _fixture.Specialities.Where(sp => sp.Name == "Cyber Security").Select(sp => sp.Id).ToList().FirstOrDefault();
        var abiturientIds = _fixture.Applications.Where(ap => specialityId == ap.SpecialityId).Select(ap => ap.AbiturientId).ToList();
        var query = _fixture.Abiturients.Where(ab => abiturientIds.Contains(ab.Id)).Select
            (
                ab => new
                {
                    Abiturient = ab,
                    TotalScore = _fixture.ExamResults.Where(er => er.AbiturientId == ab.Id).Sum(er => er.Result)
                }
            ).OrderByDescending(x => x.TotalScore).Select(x => x.Abiturient.Name).ToList();

        Assert.Equivalent(2, query.Count());
        Assert.Equal(new List<string> { "Anna", "Ivan" }, query);
    }

    [Fact]
    public void TestFirstPrioritySpecialitiesByAbiturientsAmount()
    {
        var query = _fixture.Applications.Where(a => a.SpecialityId == 0 && a.Priority == 1).ToList().Count();
        var query2 = _fixture.Applications.Where(a => a.SpecialityId == 8 && a.Priority == 1).ToList().Count();

        Assert.Equivalent(2, query);
        Assert.Equivalent(1, query2);
    }

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

        Assert.Equivalent(5, query.Count());
        Assert.Equal(new List<string> { "Anna", "Ivan", "Natalia", "Olga", "Pasha"  }, query);
    }

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

        Assert.Equivalent(9,  query.Count());
        Assert.Equal(new List<string>{ "Anna", "Natalia", "Anna", "Olga", "Anna", "Natalia", "Ivan", "Ivan", "Dmitry" },  query.Select(q => q.Abiturient.Name).ToList());
        Assert.Equal(new List<int>{ 0, 4, 0, 1, 0, 4, 9, 9, 8 },  query.Select(q => q.SpecialityId).ToList());
    }
}