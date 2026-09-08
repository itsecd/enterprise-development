using AutoService.Domain.Enums;

namespace AutoService.Domain.Entities;

public class Mechanic
{
    public int Id { get; set; }

    public string PassportNumber { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public MechanicSpecialization Specialization { get; set; }

    public int Experience { get; set; }


    public List<OrderMechanic> Orders { get; set; } = [];
}