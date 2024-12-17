using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class ChatEntityTypeConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.SenderName).IsRequired();
            builder.Property(x => x.ReceiverName).IsRequired();

            builder.Property(x => x.SenderID).IsRequired();

            builder.Property(x => x.ReceiverId).IsRequired();

            builder.Property(x => x.DateTime).IsRequired().HasDefaultValue(DateTime.Now);

            builder.Property(x => x.IsRead).IsRequired().HasDefaultValue(false);



           

        }
    }
}

