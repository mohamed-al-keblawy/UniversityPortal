using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityPortal.Models.Entities
{
    public class AssessmentCriterion
    {
        public int CriterionId { get; set; }
        public int AssessmentId { get; set; }
        public string CriterionName { get; set; }
        public int Score { get; set; }
        public string Remarks { get; set; }
    }

}
