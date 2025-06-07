using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityPortal.Models.Entities
{
    public class Assessment
    {
        public int AssessmentId { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public int AssessedBy { get; set; }
        public List<AssessmentCriterion> Criteria { get; set; }
    }

}
