  

namespace schoolProject1122.Models
{
    public class TeacherSubjectClass
    {

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = default!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = default!;

        public int ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; } = default!;

    }
}
