using Microsoft.EntityFrameworkCore;
using schoolProject1122.Models;


namespace schoolProject1122.Data
{
    public class ApplicationDbContext: DbContext

    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<SubjectLanguage> SubjectLanguages => Set<SubjectLanguage>();
        public DbSet<TeacherSubjectClass> TeacherSubjectClasses => Set<TeacherSubjectClass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique roll number per class (common real-world constraint)
            modelBuilder.Entity<Student>()
                .HasIndex(s => new { s.ClassRoomId, s.RollNumber })
                .IsUnique();

            // SubjectLanguage composite key
            modelBuilder.Entity<SubjectLanguage>()
                .HasKey(sl => new { sl.SubjectId, sl.LanguageId });

            // TeacherSubjectClass composite key
            modelBuilder.Entity<TeacherSubjectClass>()
                .HasKey(tsc => new { tsc.TeacherId, tsc.SubjectId, tsc.ClassRoomId });

            // Relationships
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.ClassRoom)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.ClassRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.ClassRoom)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassRoomId)
                .OnDelete(DeleteBehavior.SetNull);
        }






    }
}
