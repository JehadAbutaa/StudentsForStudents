using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;


namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class ChatViewModelEntityTypeConfig : IEntityTypeConfiguration<ChatViewModel>
    {
        public void Configure(EntityTypeBuilder<ChatViewModel> builder)
        {
          

            builder.Property(x => x.ChatHistory).IsRequired();
            builder.Property(x => x.CurrentChat).IsRequired();




        }
    }
}

