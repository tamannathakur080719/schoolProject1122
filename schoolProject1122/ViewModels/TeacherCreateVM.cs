using System.ComponentModel.DataAnnotations;


namespace schoolProject1122.ViewModels
{
    public class TeacherCreateVM
    {
        [Required, StringLength(60)]
        public string Name { get; set; } = default!;

        [Required, Range(18, 100)]
        public int Age { get; set; }

        [Required, StringLength(10)]
        public string Sex { get; set; } = default!;

        public IFormFile? Image { get; set; }

    }
}
