using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.BLL.Interfaces
{
    public interface IAssignmentService
    {
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<Assignment> GetByIdAsync(int id);
        Task<int> CreateAssignmentAsync(Assignment assignment);
    }
}
