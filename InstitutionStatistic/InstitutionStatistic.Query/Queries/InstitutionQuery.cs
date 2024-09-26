using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models;


namespace InstitutionStatistic.Query.Queries;
/// <summary>
/// Запросы об университетах
/// </summary>
public class InstitutinQuery: GetInfoQuery<Institution>
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="repository"></param>
    public InstitutinQuery(ICollection<Institution> repository): base(repository) {}

    /// <summary>
    /// Вывести информацию о факультетах данного института
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public List<Faculty> GetInstitutionFaculties<T>(Func<Institution, T> selector, T value)
    {
        return Repository
            .Where(x => selector(x)?.Equals(value) == true)
            .SelectMany(x => x.Faculties)
            .ToList();
    }

    /// <summary>
    /// Вывести информацию о кафедрах данного института
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public List<Department> GetInstitutionDepartments<T>(Func<Institution, T> selector, T value)
    {
        return Repository
            .Where(x => selector(x)?.Equals(value) == true)
            .SelectMany(x => x.Faculties)
            .SelectMany(x => x.Departments)
            .ToList();
    }

    /// <summary>
    /// Вывести информацию о специальностях данного института
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public List<Speciality> GetInstitutionSpecialities<T>(Func<Institution, T> selector, T value)
    {
        return Repository
            .Where(x => selector(x)?.Equals(value) == true)
            .SelectMany(x => x.Faculties)
            .SelectMany(x => x.Departments)
            .SelectMany(x => x.Groups)
            .Select(x => x.Speciality)
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// Получить вузы с максимальным кол-ом кафедр, упорядочить по названию
    /// </summary>
    /// <returns></returns>
    public List<Institution> GetMaxDepartmentInstitutions()
    {
        var maxDepartmentsCount = Repository
            .Max(x => x.Faculties.Sum(y => y.Departments.Count));

        return Repository
            .Where(x => x.Faculties.Sum(y => y.Departments.Count) == maxDepartmentsCount)
            .OrderBy(x => x.Name)
            .ToList();
    }

    /// <summary>
    /// Получить институты с заданной собстввенностью учреждения и количество групп
    /// </summary>
    /// <param name="institutionOwnership"></param>
    /// <param name="groupsCount"></param>
    /// <returns></returns>
    public List<Institution> GetInstitutions(InstitutionOwnership institutionOwnership, int groupsCount)
    {
        return Repository
            .Where(x => x.InstitutionOwnership == institutionOwnership)
            .Where(x => x.Faculties.Sum(y => y.Departments.Sum(z => z.Groups.Count)) == groupsCount)
            .ToList();
    }

    /// <summary>
    /// Получить кол-во факультетов по каждому типу собственности учреждения и собиственности здания
    /// </summary>
    /// <param name="institutionOwnership"></param>
    /// <param name="buildingOwnership"></param>
    /// <returns></returns>
    public int GetFacultiesCountByOwnership(
        InstitutionOwnership institutionOwnership,
        BuildingOwnership buildingOwnership)
    {
        return Repository
            .Where(x => x.InstitutionOwnership == institutionOwnership)
            .Where(x => x.BuildingOwnership == buildingOwnership)
            .Sum(x => x.Faculties.Count);
    }

    /// <summary>
    /// Получить кол-во кафедр по каждому типу собственности учреждения и собиственности здания
    /// </summary>
    /// <param name="institutionOwnership"></param>
    /// <param name="buildingOwnership"></param>
    /// <returns></returns>
    public int GetDepartmentsCountByOwnership(
        InstitutionOwnership institutionOwnership,
        BuildingOwnership buildingOwnership)
    {
        return Repository
             .Where(x => x.InstitutionOwnership == institutionOwnership)
             .Where(x => x.BuildingOwnership == buildingOwnership)
             .Sum(x => x.Faculties.Sum(y => y.Departments.Count));
    }

    /// <summary>
    /// Получить кол-во специальностей по каждому типу собственности учреждения и собиственности здания
    /// </summary>
    /// <param name="institutionOwnership"></param>
    /// <param name="buildingOwnership"></param>
    /// <returns></returns>
    public int GetSpecialitiesCountByOwnership(
        InstitutionOwnership institutionOwnership,
        BuildingOwnership buildingOwnership)
    {
        return Repository
            .Where(x => x.InstitutionOwnership == institutionOwnership)
            .Where(x => x.BuildingOwnership == buildingOwnership)
            .SelectMany(x => x.Faculties)
            .SelectMany(faculty => faculty.Departments)
            .SelectMany(department => department.Groups)
            .Select(group => group.Speciality.Name)
            .Distinct()
            .Count();
    }
}
