
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderName).HasMaxLength(200).IsRequired();

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(i => i.OrderId);


        builder.ComplexProperty(o => o.Payment, pBuilder =>
        {
            pBuilder.Property(p => p.CardName).HasMaxLength(50);
            pBuilder.Property(p => p.CardNumber).HasMaxLength(24);
            pBuilder.Property(p => p.Expiration).HasMaxLength(10);
            pBuilder.Property(p => p.CVV).HasMaxLength(3);
            pBuilder.Property(p => p.PaymentMethod);
        });

        builder.ComplexProperty(o => o.PaymentAddress, paBuilder =>
        {
            paBuilder.Property(pa => pa.City).HasMaxLength(100).IsRequired();
            paBuilder.Property(pa => pa.Country).HasMaxLength(60).IsRequired();
            paBuilder.Property(pa => pa.ZipCode).HasMaxLength(5).IsRequired();
        });
        builder.ComplexProperty(o => o.ShippingAddress, paBuilder =>
        {
            paBuilder.Property(sa => sa.City).HasMaxLength(100).IsRequired();
            paBuilder.Property(sa => sa.Country).HasMaxLength(60).IsRequired();
            paBuilder.Property(sa => sa.ZipCode).HasMaxLength(5).IsRequired();
        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Pending)
            .HasConversion(
            sToDb => sToDb.ToString(), 
            sFromDb => Enum.Parse<OrderStatus>(sFromDb));

        builder.Property(o => o.TotalAmount);
    }
    
}
