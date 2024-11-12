using Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole
                {
                    //Id = Guid.NewGuid().ToString(),
                    Name = RoleEnum.SuperAdmin.ToString(),
                    NormalizedName = RoleEnum.SuperAdmin.ToString().ToUpper(),
                    DateCreated = DateTime.UtcNow
                },
                new UserRole
                {
                    //Id = Guid.NewGuid().ToString(),
                    Name = RoleEnum.Admin.ToString(),
                    NormalizedName = RoleEnum.Admin.ToString().ToUpper(),
                    DateCreated = DateTime.UtcNow
                },
                new UserRole
                {
                    //Id = Guid.NewGuid().ToString(),
                    Name = RoleEnum.User.ToString(),
                    NormalizedName = RoleEnum.User.ToString().ToUpper(),
                    DateCreated = DateTime.UtcNow
                }
           );
        }
    }
}
