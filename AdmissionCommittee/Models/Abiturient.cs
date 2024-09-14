using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Models
{
    public class Abiturient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<ExamResults> ExamResults { get; set; } = new ();
        public List<Applications> Applications { get; set; } = new ();
    }
}
