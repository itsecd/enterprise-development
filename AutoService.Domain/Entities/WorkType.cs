using AutoService.Domain.Enums;

namespace AutoService.Domain.Entities;

public class WorkType
{
    public int Id { get; set; }


    public string Name { get; set; } = string.Empty;


    public WorkCategory Category { get; set; }


    public decimal Cost { get; set; }


    public TimeSpan Duration { get; set; }


    public string Description { get; set; } = string.Empty;


    public List<OrderWork> Orders { get; set; } = [];
}