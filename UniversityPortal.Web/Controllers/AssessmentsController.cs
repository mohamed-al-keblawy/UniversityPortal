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

    public async Task<IActionResult> Index(int? assignmentId = null)
    {
        // For demo: show assessments for all students or one assignment
        int facultyId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        // You may fetch students from submissions, this is demo only
        var allAssessments = await _assessmentService.GetByStudentIdAsync(-1); // -1 = get all
        var myAssessments = allAssessments.Where(a => a.AssessedBy == facultyId).ToList();

        ViewBag.MyAssessments = myAssessments;
        return View();
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
