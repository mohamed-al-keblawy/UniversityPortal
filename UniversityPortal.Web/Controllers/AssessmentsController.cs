using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityPortal.BLL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;

[Authorize(Roles = "Faculty")]
public class AssessmentsController : Controller
{
    private readonly IAssessmentService _assessmentService;
    private readonly IAssignmentService _assignmentService;

    public AssessmentsController(IAssessmentService assessmentService, IAssignmentService assignmentService)
    {
        _assessmentService = assessmentService;
        _assignmentService = assignmentService;
    }

    public async Task<IActionResult> Index(int studentId)
    {
        var assessments = await _assessmentService.GetByStudentIdAsync(studentId);
        return View(assessments);
    }

    public IActionResult Create(int assignmentId, int studentId)
    {
        var model = new Assessment
        {
            AssignmentId = assignmentId,
            StudentId = studentId,
            Criteria = new List<AssessmentCriterion>
            {
                new AssessmentCriterion { CriterionName = "Clarity" },
                new AssessmentCriterion { CriterionName = "Completeness" },
                new AssessmentCriterion { CriterionName = "Originality" }
            }
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Assessment assessment)
    {
        if (!ModelState.IsValid) return View(assessment);

        assessment.AssessedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        await _assessmentService.CreateAssessmentAsync(assessment);
        return RedirectToAction("Index", new { studentId = assessment.StudentId });
    }
}
