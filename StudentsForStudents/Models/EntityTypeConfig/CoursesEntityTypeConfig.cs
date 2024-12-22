using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class CoursesEntityTypeConfig : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.CourcePicture).IsRequired(false);

            




        }
    }
}

