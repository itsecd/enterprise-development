using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;
public class Benefit
{
    public int Id { get; set; }
    public string Type { get; set; }
    public Employee Employee { get; set; }
    public DateTime Date { get; set; }
}
