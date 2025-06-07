using UniversityPortal.BLL.Interfaces;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            return await _assignmentRepository.GetAllAsync();
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            return await _assignmentRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAssignmentAsync(Assignment assignment)
        {
            if (string.IsNullOrWhiteSpace(assignment.Title))
                throw new ArgumentException("Title is required");

            return await _assignmentRepository.CreateAsync(assignment);
        }
    }
}
