using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityPortal.BLL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;

[Authorize(Roles = "Student")]
public class SubmissionsController : Controller
{
    private readonly ISubmissionService _submissionService;
    private readonly IAssignmentService _assignmentService;

    public SubmissionsController(ISubmissionService submissionService, IAssignmentService assignmentService)
    {
        _submissionService = submissionService;
        _assignmentService = assignmentService;
    }

    public async Task<IActionResult> Index()
    {
        int studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var submissions = await _submissionService.GetByStudentAsync(studentId);
        return View(submissions);
    }

    public async Task<IActionResult> Submit(int assignmentId)
    {
        var assignment = await _assignmentService.GetByIdAsync(assignmentId);
        ViewBag.Assignment = assignment;
        return View(new Submission { AssignmentId = assignmentId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(Submission submission)
    {
        if (!ModelState.IsValid) return View(submission);

        submission.SubmittedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        submission.SubmissionDate = DateTime.Now;

        await _submissionService.SubmitAsync(submission);
        return RedirectToAction("Index");
    }
}
