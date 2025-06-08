using UniversityPortal.BLL.Interfaces;
using UniversityPortal.DAL;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository _repository;

    public SubmissionService(ISubmissionRepository repository)
    {
        _repository = repository;
    }

    public Task<int> SubmitAsync(Submission submission)
    {
        // Optionally add file type/date validation
        return _repository.SubmitAsync(submission);
    }

    public Task<IEnumerable<Submission>> GetByStudentAsync(int studentId)
    {
        return _repository.GetByStudentIdAsync(studentId);
    }
}
