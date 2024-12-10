using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Repo.EntityFrameWork.Data.Configuration
{
	public class GroupConfig : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder) {
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.GroupName).HasColumnType("VARCHAR").IsRequired().HasMaxLength(64);
			builder.Property(x => x.Description).HasColumnType("VARCHAR").IsRequired(false).HasMaxLength(256);
			builder.Property(x => x.CreatedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETUTCDATE()"); // SQL Server function for UTC datetime

			builder.ToTable("Groups");
		}
	}
}
