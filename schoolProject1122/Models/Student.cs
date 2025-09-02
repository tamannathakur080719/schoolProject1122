using System.ComponentModel.DataAnnotations;

namespace schoolProject1122.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = default!;

        [Required]
        [Range(3, 100)]
        public int Age { get; set; }

        [Required]
        [Range(1, 10000)]
        public int RollNumber { get; set; }

        public int? ClassRoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }

        [StringLength(260)]
        public string? ImagePath { get; set; }

    }
}
