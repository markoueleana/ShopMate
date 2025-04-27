
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
       builder.HasKey(i => i.Id);

       builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(i => i.ProductId);
       builder.Property(i => i.Quantity).IsRequired();
       builder.Property(i => i.Price).IsRequired();
    }
}
