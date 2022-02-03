﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Ef.Configurations
{
    public class UserRoleConfiguration: IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            var b = builder;

            b.Property(_ => _.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            b.Property(_ => _.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            b.Property(_ => _.DateCreated)
                .HasColumnName("date_created")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            b.ToTable("user_role");
            b.HasKey(_ => new {_.UserId, _.RoleId});
        }
    }
    
    public class RolePermissionConfiguration: IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            var b = builder;

            b.Property(_ => _.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            b.Property(_ => _.PermissionId)
                .HasColumnName("permission_id")
                .IsRequired();

            b.Property(_ => _.DateCreated)
                .HasColumnName("date_created")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            b.ToTable("role_permission");
            b.HasKey(_ => new {_.RoleId, _.PermissionId});
        }
    }
}