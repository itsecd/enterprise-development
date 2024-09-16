namespace AdmissionCommittee.Domain.Models
{
    public class Speciality
    {
        public required int Id { get; set; }
        public required string Number { get; set; }
        public required string Name { get; set; }
        public required string Facility { get; set; }
    }
}
