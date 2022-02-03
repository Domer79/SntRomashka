﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Ef.Configurations
{
    public class PermissionConfiguration: IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            var b = builder;

            b.ToTable("permission");

            b.Property(_ => _.Id)
                .HasColumnName("id");

            b.Property(_ => _.Politic)
                .HasColumnName("politic")
                .IsRequired();

            b.Property(_ => _.Name)
                .HasColumnName("name");
            
            b.Property(_ => _.DateCreated)
                .HasColumnName("date_created")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            b.HasKey(_ => _.Id);
            b.HasIndex(_ => _.Politic)
                .IsUnique()
                .HasDatabaseName("UQ_permission_politic");
        }
    }
}