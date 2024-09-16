namespace AdmissionCommittee.Domain.Models
{
    public class Abiturient
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime BirthdayDate { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public List<ExamResult> ExamResults { get; set; } = new();
        public List<Application> Applications { get; set; } = new();
    }
}
