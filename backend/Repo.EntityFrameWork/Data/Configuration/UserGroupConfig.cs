using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Repo.EntityFrameWork.Data.Configuration
{
	public class UserGroupConfig : IEntityTypeConfiguration<UserGroup>
	{
		public void Configure(EntityTypeBuilder<UserGroup> builder) {
			builder.HasKey(x => new { x.UserId, x.GroupId });
			builder.HasOne(x => x.User).WithMany(x => x.UserGroups).HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.Group).WithMany(x => x.UserGroups).HasForeignKey(x => x.GroupId);

			builder.Property(x => x.JoinAt).HasColumnType("DATETIME").HasDefaultValueSql("GETUTCDATE()");
			builder.ToTable("UsersGroups");
		}
	}
}
