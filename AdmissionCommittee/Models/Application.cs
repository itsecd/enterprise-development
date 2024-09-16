namespace AdmissionCommittee.Domain.Models
{
    public class Application
    {
        public required int Id { get; set; }
        public required int SpecialityId { get; set; }
        public required int AbiturientId { get; set; }
        public required int Priority { get; set; }
    }
}
