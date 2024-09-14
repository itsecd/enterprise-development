using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Models
{
    public class ExamResults
    {
        public int Id { get; set; }
        public int AbiturientId { get; set; }
        public string ExamName { get; set; }
        public int ExamResult {  get; set; }
    }
}
