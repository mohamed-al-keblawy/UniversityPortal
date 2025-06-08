using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.DAL.Interfaces
{
    public interface ISubmissionRepository
    {
        Task<int> SubmitAsync(Submission submission);
        Task<IEnumerable<Submission>> GetByStudentIdAsync(int studentId);
    }
}
