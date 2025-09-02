using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace schoolProject1122.ViewModels
{
    public class StudentCreateVM
    {
        [Required, StringLength(60)]
        public string Name { get; set; } = default!;

        [Required, Range(3, 100)]
        public int Age { get; set; }

        [Required, Range(1, 10000)]
        public int RollNumber { get; set; }

        public int? ClassRoomId { get; set; }

        public IFormFile? Image { get; set; }

    }
}
