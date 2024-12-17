using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentsForStudents.Models;

public class TeacherCourseConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasMany(t => t.InrolmentCourses)
               .WithMany(c => c.Teachers)
               .UsingEntity(j => j.ToTable("TeacherCourses")); 
    }
}
