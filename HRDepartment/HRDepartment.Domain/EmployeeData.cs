using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDepartment.Domain;
public class EmployeeData
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public DateTime HireDate { get; set; }
    public string HomeAddress { get; set; }
    public string WorkPhone { get; set; }
    public string HomePhone { get; set; }
    public string FamilyStatus { get; set; }
    public int FamilyMembers { get; set; }
    public int Children { get; set; }
    public string Position { get; set; }
}