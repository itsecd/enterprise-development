namespace CarWash.Contracts.Dto;

public class TopClientDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int VisitCount { get; set; }
}

public class CurrentCarDto
{
    public string LicensePlate { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public int BoxNumber { get; set; }
}

public class TopServiceDto
{
    public string Name { get; set; } = string.Empty;
    public int UsageCount { get; set; }
}

public class BoxFreeTimeDto
{
    public int BoxNumber { get; set; }
    public DateTime FreeTime { get; set; }
}