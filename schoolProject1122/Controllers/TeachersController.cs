using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolProject1122.Data;
using schoolProject1122.Models;
using schoolProject1122.ViewModels;
using System;

namespace schoolProject1122.Controllers
{
    public class TeachersController : Controller

    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<TeachersController> _logger;
        private readonly IWebHostEnvironment _env;

        public TeachersController(ApplicationDbContext ctx, ILogger<TeachersController> logger, IWebHostEnvironment env)
        {
            _ctx = ctx;
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public IActionResult Create() 
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string? imagePath = null;
            if (vm.Image is { Length: > 0 })
            {
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "teachers");
                Directory.CreateDirectory(uploadsDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.Image.FileName)}";
                var fullPath = Path.Combine(uploadsDir, fileName);
                using var stream = System.IO.File.Create(fullPath);
                await vm.Image.CopyToAsync(stream);
                imagePath = $"/uploads/teachers/{fileName}";
            }

            var teacher = new Teacher
            {
                Name = vm.Name,
                Age = vm.Age,
                Sex = vm.Sex,
                ImagePath = imagePath
            };

            _ctx.Teachers.Add(teacher);
            try
            {
                await _ctx.SaveChangesAsync();
                TempData["msg"] = "Teacher created successfully.";
                return RedirectToAction("Index", "Students");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating teacher");
                ModelState.AddModelError("", "Could not save teacher.");
                return View(vm);
            }
        }
       
    }
}
