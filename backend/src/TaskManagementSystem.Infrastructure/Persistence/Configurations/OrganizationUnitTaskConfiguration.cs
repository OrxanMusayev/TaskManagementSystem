using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Domain.TaskManagement.Entities;
using TaskManagementSystem.Domain.TaskManagement.Enums;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations
{
    public class OrganizationUnitTaskConfiguration : IEntityTypeConfiguration<OrganizationUnitTask>
    {
        public void Configure(EntityTypeBuilder<OrganizationUnitTask> builder)
        {
            builder.ToTable("Tasks");
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Deadline).IsRequired();
            builder.Property(p => p.Status).HasDefaultValue(TaskStatus.ToDo);
        }
    }
}
