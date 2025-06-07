using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.DAL.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAllAsync();
        Task<Assignment> GetByIdAsync(int id);
        Task<int> CreateAsync(Assignment assignment);
    }

}
