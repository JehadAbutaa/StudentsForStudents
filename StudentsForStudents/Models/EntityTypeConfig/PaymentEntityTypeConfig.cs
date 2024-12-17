using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace StudentsForStudents.Models.EntityTypeConfig
{
    public class PaymentEntityTypeConfig : IEntityTypeConfiguration<Payment>
    {

        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            // builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.status).IsRequired().HasMaxLength(100);

            builder.Property(x => x.message).IsRequired();




        }

    }
}
