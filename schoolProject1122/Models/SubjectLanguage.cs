namespace schoolProject1122.Models
{
    public class SubjectLanguage
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = default!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = default!;

    }
}
