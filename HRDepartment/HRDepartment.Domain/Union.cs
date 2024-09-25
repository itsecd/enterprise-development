using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;
public class Union
{
    public int Id { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Benefit> Benefits { get; set; }
}