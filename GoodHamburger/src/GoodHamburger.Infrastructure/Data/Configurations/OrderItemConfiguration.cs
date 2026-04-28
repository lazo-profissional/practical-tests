using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoodHamburger.Domain.Models;

namespace GoodHamburger.Infrastructure.Data;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
            .ValueGeneratedOnAdd();

        builder.Property(oi => oi.MenuItemId)
            .IsRequired();

        builder.Property(oi => oi.MenuItemName)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(oi => oi.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(oi => oi.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property<int>("OrderId")
            .IsRequired();
    }
}