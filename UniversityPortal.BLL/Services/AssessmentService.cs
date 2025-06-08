using UniversityPortal.BLL.Interfaces;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models.Entities;

public class AssessmentService : IAssessmentService
{
    private readonly IAssessmentRepository _repository;

    public AssessmentService(IAssessmentRepository repository)
    {
        _repository = repository;
    }

    public Task<int> CreateAssessmentAsync(Assessment assessment)
    {
        // Optionally validate scores and criteria before creating
        return _repository.CreateAssessmentAsync(assessment);
    }

    public Task<IEnumerable<Assessment>> GetByStudentIdAsync(int studentId)
    {
        return _repository.GetByStudentIdAsync(studentId);
    }

    public Task<IEnumerable<PerformanceMetric>> GetStudentPerformanceAsync(int studentId)
    {
        return _repository.GetStudentPerformanceAsync(studentId);
    }

}
