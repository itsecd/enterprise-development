using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;

public class Restaurant
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public double Rating { get; set; } 
    public required string WorkingHours { get; set; }
}
