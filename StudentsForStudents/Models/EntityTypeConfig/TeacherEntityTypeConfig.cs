using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class TeacherEntityTypeConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

            builder.Property(x=>x.Desc).IsRequired().HasMaxLength(200);

            builder.Property(x => x.QualificationCourses).IsRequired(false);


            builder.Property(x => x.GPA).IsRequired();

            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

            builder.Property(x => x.StudentId).IsRequired().HasMaxLength(12);

            builder.Property(x => x.Major).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Level).IsRequired();

            builder.Property(x => x.ImgPath).IsRequired(false);

        }
    }
}

