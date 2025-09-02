using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolProject1122.Data;
using schoolProject1122.Models;
using schoolProject1122.ViewModels;
using Microsoft.AspNetCore.Http;
using System;

namespace schoolProject1122.Controllers
{
    public class StudentsController : Controller
    {
       
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<StudentsController> _logger;
        private readonly IWebHostEnvironment _env;

        public StudentsController(ApplicationDbContext ctx, ILogger<StudentsController> logger, IWebHostEnvironment env)
        {
            _ctx = ctx;
            _logger = logger;
            _env = env;
        }
        public  async Task<IActionResult> Index(string? search)
        {
            var query = _ctx.Students
               .Include(s => s.ClassRoom)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            var students = await query
                .OrderBy(s => s.ClassRoom!.Name)
                .ThenBy(s => s.RollNumber)
                .ToListAsync();

            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Classes = await _ctx.ClassRooms.OrderBy(c => c.Name).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCreateVM vm)
        {
            ViewBag.Classes = await _ctx.ClassRooms.OrderBy(c => c.Name).ToListAsync();
            if (!ModelState.IsValid) return View(vm);

            string? imagePath = null;
            if (vm.Image is { Length: > 0 })
            {
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "students");
                Directory.CreateDirectory(uploadsDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.Image.FileName)}";
                var fullPath = Path.Combine(uploadsDir, fileName);
                using var stream = System.IO.File.Create(fullPath);
                await vm.Image.CopyToAsync(stream);
                imagePath = $"/uploads/students/{fileName}";
            }

            var student = new Student
            {
                Name = vm.Name,
                Age = vm.Age,
                RollNumber = vm.RollNumber,
                ClassRoomId = vm.ClassRoomId,
                ImagePath = imagePath
            };

            _ctx.Students.Add(student);
            try
            {
                await _ctx.SaveChangesAsync();
                TempData["msg"] = "Student created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating student");
                ModelState.AddModelError("", "Could not save student. Check that roll number is unique within the class.");
                return View(vm);
            }
        }

        // 5) List all subjects of a student and their teachers
        public async Task<IActionResult> Subjects(int id)
        {
            var student = await _ctx.Students
                .Include(s => s.ClassRoom)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) return NotFound();

            var classId = student.ClassRoomId;

            var subjectsWithTeachers = await _ctx.TeacherSubjectClasses
                .Where(tsc => tsc.ClassRoomId == classId)
                .Include(tsc => tsc.Subject).ThenInclude(s => s.SubjectLanguages).ThenInclude(sl => sl.Language)
                .Include(tsc => tsc.Teacher)
                .OrderBy(tsc => tsc.Subject.Name)
                .ToListAsync();

            ViewBag.Student = student;
            return View(subjectsWithTeachers);
        }


    }

}
