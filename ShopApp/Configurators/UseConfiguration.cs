using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App.Configurators;

public class UseConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure (EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("SYSDATETIME()");

    }
}
