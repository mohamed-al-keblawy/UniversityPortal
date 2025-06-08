using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityPortal.BLL.Interfaces;

[Authorize(Roles = "Faculty")]
public class FacultyController : Controller
{
    private readonly IAssignmentService _assignmentService;
    private readonly IAssessmentService _assessmentService;

    public FacultyController(IAssignmentService assignmentService, IAssessmentService assessmentService)
    {
        _assignmentService = assignmentService;
        _assessmentService = assessmentService;
    }

    public async Task<IActionResult> Index()
    {
        int facultyId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var assignments = await _assignmentService.GetAllAssignmentsAsync();
        var totalAssignments = assignments.Count(a => a.CreatedBy == facultyId);

        // Since assessment service doesn't filter by AssessedBy, we filter manually
        var assessments = await _assessmentService.GetByStudentIdAsync(-1); // fake ID to get all
        var totalAssessments = assessments.Count(a => a.AssessedBy == facultyId);

        ViewBag.TotalAssignments = totalAssignments;
        ViewBag.TotalAssessments = totalAssessments;

        return View();
    }
}
