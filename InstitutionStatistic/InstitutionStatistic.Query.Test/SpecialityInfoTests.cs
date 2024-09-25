using InstitutionStatistic.Query.Queries;

namespace InstitutionStatistic.Query.Test;

/// <summary>
/// тесты для проверки запросов о специальностях
/// </summary>

public class SpecialityInfoTests: TestBase
{
    private SpecialityQuery _specialitiesQuery;

    public SpecialityInfoTests()
    {
        _specialitiesQuery = new SpecialityQuery(Specialities);
    }


    #region Вывести информацию о топ 5 популярных специальностях
    [Fact]
    public void GetTopFiveSpecialitiesTest()
    {
        Assert.Equal(
            _specialitiesQuery.GetTopFiveSpecialities().Select(x => x.Name).ToList(),
            ["SPEC1", "SPEC2", "SPEC3", "SPEC4", "SPEC5"]);
    }
    #endregion
}
