using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace schoolProject1122.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = default!;

        [Required]

        public int ClassRoomId { get; set; }
        
        public ClassRoom? ClassRoom { get; set; }   

        public ICollection<SubjectLanguage> SubjectLanguages { get; set; } = new List<SubjectLanguage>();


        public ICollection<TeacherSubjectClass> TeachersSubjectClasses { get; set; }  = new List<TeacherSubjectClass>();

        
    }
}
