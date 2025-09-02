using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolProject1122.Data;
using schoolProject1122.Models;
using schoolProject1122.ViewModels;


namespace schoolProject1122.Controllers
{
    public class SubjectsController : Controller
    {

        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(ApplicationDbContext ctx, ILogger<SubjectsController> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Classes = await _ctx.ClassRooms.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Languages = await _ctx.Languages.OrderBy(l => l.Name).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectCreateVM vm)
        {
            ViewBag.Classes = await _ctx.ClassRooms.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Languages = await _ctx.Languages.OrderBy(l => l.Name).ToListAsync();
            if (!ModelState.IsValid) return View(vm);

            var subject = new Subject
            {
                Name = vm.Name,
                ClassRoomId = (int)vm.ClassRoomId,
                SubjectLanguages = vm.LanguageIds.Select(id => new SubjectLanguage { LanguageId = id }).ToList()
            };

            _ctx.Subjects.Add(subject);

            try
            {
                await _ctx.SaveChangesAsync();
                TempData["msg"] = "Subject created successfully.";
                return RedirectToAction("Index", "Students");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating subject");
                ModelState.AddModelError("", "Could not save subject.");
                return View(vm);
            }
        }
    }
}