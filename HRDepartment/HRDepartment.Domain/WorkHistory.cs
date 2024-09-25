using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;
public class WorkHistory
{
    public int Id { get; set; }
    public Employee Employee { get; set; }
    public Position Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}