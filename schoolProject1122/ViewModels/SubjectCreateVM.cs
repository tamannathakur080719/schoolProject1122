using System.ComponentModel.DataAnnotations;
namespace schoolProject1122.ViewModels
{
    public class SubjectCreateVM
    {
        internal object ClassRoomId;

        [Required, StringLength(60)]
        public string Name { get; set; } = default!;

        [Required]
        public int ClassRoom { get; set; }

        // Multi-select languages (IDs)
        public List<int> LanguageIds { get; set; } = new();
    }
}
