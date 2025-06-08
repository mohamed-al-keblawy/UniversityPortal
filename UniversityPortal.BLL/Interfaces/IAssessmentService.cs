using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.BLL.Interfaces
{
    public interface IAssessmentService
    {
        Task<int> CreateAssessmentAsync(Assessment assessment);
        Task<IEnumerable<Assessment>> GetByStudentIdAsync(int studentId);
    }
}
