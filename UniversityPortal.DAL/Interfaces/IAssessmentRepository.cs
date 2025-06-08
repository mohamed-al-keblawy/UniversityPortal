using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.DAL.Interfaces
{
    public interface IAssessmentRepository
    {
        Task<int> CreateAssessmentAsync(Assessment assessment);
        Task<IEnumerable<Assessment>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<PerformanceMetric>> GetStudentPerformanceAsync(int studentId);

    }
}
