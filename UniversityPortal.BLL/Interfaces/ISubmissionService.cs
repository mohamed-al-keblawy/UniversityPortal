using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.BLL.Interfaces
{
    public interface ISubmissionService
    {
        Task<int> SubmitAsync(Submission submission);
        Task<IEnumerable<Submission>> GetByStudentAsync(int studentId);
    }
}
