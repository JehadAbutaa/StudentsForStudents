using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class EventEntityTypeConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.EventID);
            builder.Property(x => x.EventID).UseIdentityColumn();

            builder.Property(x => x.Title);

            builder.Property(x => x.StartDate);





        }
    }
}

