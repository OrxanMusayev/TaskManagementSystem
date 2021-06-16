using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations
{
    public class UserOrganizationUnitConfiguration: IEntityTypeConfiguration<UserOrganizationUnit>
    {
        public void Configure(EntityTypeBuilder<UserOrganizationUnit> builder)
        {
            builder.ToTable("UserOrganizationUnits");

            builder.HasKey(t => new { t.UserId, t.OrganizationUnitId });

        }
    }
}
