using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace schoolProject1122.Models
{
    public class Language
    {
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Name { get; set; } = default!;

        public ICollection<SubjectLanguage> SubjectLanguages { get; set; } = new List<SubjectLanguage>();
    }
}
