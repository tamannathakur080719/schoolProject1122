using Microsoft.EntityFrameworkCore;
using schoolProject1122.Models;
using System;

namespace schoolProject1122.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext ctx)
        {
            if (await ctx.ClassRooms.AnyAsync()) return; // already seeded

            var classes = new[]
            {
                new ClassRoom { Name = "Class 1" },
                new ClassRoom { Name = "Class 2" },
                new ClassRoom { Name = "Class 3" },
                new ClassRoom { Name = "Class 4" }
            };
            ctx.ClassRooms.AddRange(classes);

            var languages = new[]
            {
                new Language { Name = "English" },
                new Language { Name = "Hindi" },
                new Language { Name = "Punjabi" }
            };
            ctx.Languages.AddRange(languages);

            await ctx.SaveChangesAsync();

            // Optional: seed a subject and a teacher mapping example
            var math = new Subject { Name = "Mathematics", ClassRoomId = classes[0].Id };
            math.SubjectLanguages.Add(new SubjectLanguage { LanguageId = languages[0].Id });
            math.SubjectLanguages.Add(new SubjectLanguage { LanguageId = languages[1].Id });

            var sci = new Subject { Name = "Science", ClassRoomId = classes[0].Id };
            sci.SubjectLanguages.Add(new SubjectLanguage { LanguageId = languages[0].Id });

            ctx.Subjects.AddRange(math, sci);

            var t1 = new Teacher { Name = "A. Sharma", Age = 32, Sex = "Male" };
            var t2 = new Teacher { Name = "R. Kaur", Age = 29, Sex = "Female" };
            ctx.Teachers.AddRange(t1, t2);

            await ctx.SaveChangesAsync();

            ctx.TeacherSubjectClasses.AddRange(
                new TeacherSubjectClass { TeacherId = t1.Id, SubjectId = math.Id, ClassRoomId = classes[0].Id },
                new TeacherSubjectClass { TeacherId = t2.Id, SubjectId = sci.Id, ClassRoomId = classes[0].Id }
            );

            await ctx.SaveChangesAsync();
        }



    }
}
