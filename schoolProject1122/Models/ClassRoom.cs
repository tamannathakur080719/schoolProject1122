using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace schoolProject1122.Models
{
    public class ClassRoom
    {
        public int Id { get; set; }

        [Required, StringLength(30)]

        public string Name { get; set; }

          public ICollection<Student> Students { get; set; } = new List<Student>();
          public ICollection<TeacherSubjectClass> TeacherSubjectClasses { get; set; } = new List<TeacherSubjectClass>();
          public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    }
}
