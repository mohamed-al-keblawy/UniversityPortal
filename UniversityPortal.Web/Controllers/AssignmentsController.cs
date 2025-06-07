using Microsoft.AspNetCore.Mvc;
using UniversityPortal.BLL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.Web.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        // GET: /Assignments
        public async Task<IActionResult> Index()
        {
            var assignments = await _assignmentService.GetAllAssignmentsAsync();
            return View(assignments);
        }

        // GET: /Assignments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var assignment = await _assignmentService.GetByIdAsync(id);
            if (assignment == null) return NotFound();
            return View(assignment);
        }

        // GET: /Assignments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (!ModelState.IsValid) return View(assignment);

            await _assignmentService.CreateAssignmentAsync(assignment);
            return RedirectToAction(nameof(Index));
        }
    }
}
