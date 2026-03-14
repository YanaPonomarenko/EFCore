using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Configurators;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.UserId)
               .IsRequired();

        builder.Property(o => o.OrderDate)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(o => o.TotalAmount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(o => o.Status)
               .IsRequired()
               .HasMaxLength(50)
               .HasDefaultValue("Нове"); 

        builder.HasOne(o => o.User)
               .WithMany() 
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Restrict); 


        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}