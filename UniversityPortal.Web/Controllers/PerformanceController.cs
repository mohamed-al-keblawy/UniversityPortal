using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityPortal.BLL.Interfaces;
using UniversityPortal.Models;

[Authorize(Roles = "Student")]
public class PerformanceController : Controller
{
    private readonly IAssessmentService _assessmentService;

    public PerformanceController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    public async Task<IActionResult> Index()
    {
        int studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var performanceData = await _assessmentService.GetStudentPerformanceAsync(studentId);
        return View(performanceData);
    }
}
