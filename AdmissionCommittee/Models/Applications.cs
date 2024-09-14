using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Models
{
    public class Applications
    {
        public int Id { get; set; }
        public int SpecialityId { get; set; }
        public int AbiturientId { get; set; }
        public int Priority {  get; set; }
    }
}
