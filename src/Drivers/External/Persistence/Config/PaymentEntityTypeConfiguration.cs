using Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace External.Persistence.Config;

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");

        builder.HasKey(payment => payment.Id);
        
        builder.Property(order => order.OrderId)
            .IsRequired();
        
        builder.Property(order => order.Amount)
            .IsRequired();

        builder.Property(payment => payment.Status)
            .HasConversion(
                status => (short)status,
                value => value)
            .IsRequired();

        builder.Property(payment => payment.QrCode)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(payment => payment.CreatedAt)
            .IsRequired();
        
        builder.Property(payment => payment.UpdatedAt)
            .IsRequired();
    }
}