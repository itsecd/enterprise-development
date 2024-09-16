namespace AdmissionCommittee.Domain.Models;

public class ExamResult
{
    public required int Id { get; set; }
    public required int AbiturientId { get; set; }
    public required string ExamName { get; set; }
    public required int Result { get; set; }
}

