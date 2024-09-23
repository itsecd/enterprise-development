using Query.Impl.Models;

namespace Query.Impl.Queries
{
    /// <summary>
    /// Запросы о специальностях
    /// </summary>
    public class SpecialityQuery : GetInfoQuery<Speciality>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public SpecialityQuery(ICollection<Speciality> repository) : base(repository) { }

        /// <summary>
        /// Получить топ 5 специальностей
        /// </summary>
        /// <returns></returns>
        public List<Speciality> GetTopFiveSpecialities()
        {
            return Repository
                .OrderByDescending(x => x.Groups.Count)
                .Take(5)
                .ToList();
        }
    }
}
