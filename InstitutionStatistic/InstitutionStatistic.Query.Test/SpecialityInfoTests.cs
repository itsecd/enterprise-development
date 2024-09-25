using InstitutionStatistic.Query.Queries;

namespace InstitutionStatistic.Query.Test;

/// <summary>
/// тесты для проверки запросов о специальностях
/// </summary>
[TestFixture]
public class SpecialityInfoTests: TestBase
{
    private SpecialityQuery _specialitiesQuery;

    [SetUp]
    public void SetUp()
    {
        _specialitiesQuery = new SpecialityQuery(Specialities);
    }


    #region Вывести информацию о топ 5 популярных специальностях
    [Test]
    public void Test1()
    {
        Assert.That(
            _specialitiesQuery.GetTopFiveSpecialities().Select(x => x.Name).ToList(),
            Is.EquivalentTo(new List<string> { "SPEC1", "SPEC2", "SPEC3", "SPEC4", "SPEC5" }));
    }
    #endregion
}
