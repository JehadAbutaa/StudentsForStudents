using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class LectureEntityTypeConfig : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TeacherName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Topic).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Time).IsRequired();


            builder.Property(x => x.Type).IsRequired();

            builder.Property(x => x.StudentId).IsRequired().HasMaxLength(10);

            builder.Property(x => x.TeacherId).IsRequired().HasMaxLength(100);

           

        }
    }
}

