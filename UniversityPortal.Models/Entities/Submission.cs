using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityPortal.Models.Entities
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int SubmittedBy { get; set; }
        public string SubmissionUrl { get; set; }
        public DateTime SubmissionDate { get; set; }
    }

}
