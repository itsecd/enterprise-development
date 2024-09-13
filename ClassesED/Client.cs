using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Client
{
    public string Passport { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public Client(string passport, string fullName, DateTime birthDate)
    {
        Passport = passport;
        FullName= fullName;
        BirthDate = birthDate;
    }
}