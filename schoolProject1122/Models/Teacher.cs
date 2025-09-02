using System.ComponentModel.DataAnnotations;

namespace schoolProject1122.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = default!;

        [Required]
        [Range(18, 100)]
        public int Age { get; set; }

        [Required, StringLength(10)]
        public string Sex { get; set; } = default!; // "Male", "Female", "Other"

        [StringLength(260)]
        public string? ImagePath { get; set; }

        public ICollection<TeacherSubjectClass> TeacherSubjectClasses { get; set; } = new List<TeacherSubjectClass>();

    }
}
