using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class CourseRequestEntityTypeConfig : IEntityTypeConfiguration<CourseRequest>
    {
        public void Configure(EntityTypeBuilder<CourseRequest> builder)
        {
            builder.HasKey(x => x.RequestId);
            builder.Property(x => x.RequestId).UseIdentityColumn();

            builder.Property(x => x.CourceName).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.RequestedBy).IsRequired();

            builder.Property(x => x.Faculty).IsRequired();

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);





        }
    }
}

