using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Queries;

namespace InstitutionStatistic.Query.Test;

/// <summary>
/// тесты для проверки запросов об институтах
/// </summary>
[TestFixture]
public class InstitutionQueriesTests : TestBase
{
    private InstitutinQuery _institutinQuery;

    [SetUp]
    public void SetUp()
    {
        _institutinQuery = new InstitutinQuery(Institutions);
    }

    #region Вывести информацию о выбранном вузе
    [Test]
    public void GetInstitutionInfoTest()
    {
        var ssau = "СГАУ";
        var pguty = "ПГУТИ";
        var notExisted = "notExisted";

        Assert.That(_institutinQuery.GetByName(ssau), Is.EqualTo(Institutions[0]));
        Assert.That(_institutinQuery.GetByName(pguty), Is.EqualTo(Institutions[2]));
        Assert.That(_institutinQuery.GetByName(notExisted), Is.EqualTo(null));
    }
    #endregion

    #region Вывести информацию о факультетах, кафедрах и специальностях данного вуза
    [TestCase("СГАУ", new string[] { "FC1", "FC2" })]
    [TestCase("ПГУТИ", new string[] { "FC4" })]
    public void GetInstitutionFacultiesTest(string name, string[] expected)
    {
        Assert.That(
            _institutinQuery.GetInstitutionFaculties(x => x.Name, name).Select(x => x.Name).ToList(),
            Is.EqualTo(expected));
    }

    [TestCase("СГАУ", new string[] { "SPEC1", "SPEC2", "SPEC3" })]
    [TestCase("ПГУТИ", new string[] { "SPEC1", "SPEC2", "SPEC3", "SPEC4" })]
    public void GetInstitutionSpecialitiesTest(string name, string[] expected)
    {
        Assert.That(
            _institutinQuery.GetInstitutionSpecialities(x => x.Name, name).Select(x => x.Name).ToList(),
            Is.EqualTo(expected));
    }

    [TestCase("СГАУ", new string[] { "ГИИБ", "ИСТ" })]
    [TestCase("ПГУТИ", new string[] { "TEST1", "TEST2" })]
    public void GetInstitutionDepartmentsTest(string name, string[] expected)
    {
        Assert.That(
            _institutinQuery.GetInstitutionDepartments(x => x.Name, name).Select(x => x.Name).ToList(),
            Is.EqualTo(expected));
    }
    #endregion

    #region Вывести информацию о ВУЗах с максимальным количеством кафедр, упорядочить по названию
    [Test]
    public void GetMaxDepartmentInstitutionsTest()
    {
        Assert.That(
            _institutinQuery.GetMaxDepartmentInstitutions().Select(x => x.Name).ToList(),
            Is.EqualTo(new List<string> { "ПГУТИ", "СГАУ" }));
    }
    #endregion

    #region Вывести информацию о ВУЗах с заданной собственностью учреждения, и количество групп в ВУЗе
    [Test]
    public void GetInstitutionsTest()
    {
        Assert.That(
            _institutinQuery.GetInstitutions(InstitutionOwnership.Municipality, 5).Select(x => x.Name).ToList(),
            Is.EqualTo(new List<string> { "САМГТУ"}));
    }
    #endregion

    #region  Вывести информацию о количестве факультетов, кафедр, специальностей по каждому типу собственности учреждения и собственности здания
    [TestCase(InstitutionOwnership.Municipality, BuildingOwnership.Municipality, ExpectedResult = 1)]
    [TestCase(InstitutionOwnership.Municipality, BuildingOwnership.Federal, ExpectedResult = 2)]
    public int GetFacultiesCountByOwnershipTest(InstitutionOwnership institutionOwnership, BuildingOwnership buildingOwnership)
    {
        return _institutinQuery.GetFacultiesCountByOwnership(institutionOwnership, buildingOwnership);
    }

    [TestCase(InstitutionOwnership.Personal, BuildingOwnership.Personal, ExpectedResult = 2)]
    [TestCase(InstitutionOwnership.Personal, BuildingOwnership.Federal, ExpectedResult = 1)]
    public int GetDepartmentsCountByOwnershipTest(InstitutionOwnership institutionOwnership, BuildingOwnership buildingOwnership)
    {
        return _institutinQuery.GetDepartmentsCountByOwnership(institutionOwnership, buildingOwnership);
    }

    [TestCase(InstitutionOwnership.Municipality, BuildingOwnership.Federal, ExpectedResult = 3)]
    [TestCase(InstitutionOwnership.Municipality, BuildingOwnership.Municipality, ExpectedResult = 4)]
    public int GetSpecialitiesCountByOwnershipTest(InstitutionOwnership institutionOwnership, BuildingOwnership buildingOwnership)
    {
        return _institutinQuery.GetSpecialitiesCountByOwnership(institutionOwnership, buildingOwnership);
    }
    #endregion
}
