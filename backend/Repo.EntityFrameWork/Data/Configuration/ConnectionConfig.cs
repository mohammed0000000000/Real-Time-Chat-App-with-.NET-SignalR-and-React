using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Repo.EntityFrameWork.Data.Configuration
{
	public class ConnectionConfig : IEntityTypeConfiguration<Connection>
	{
		public void Configure(EntityTypeBuilder<Connection> builder) {
			builder.HasKey(x => x.ConnectionId);
			builder.Property(x => x.ConnectionId).ValueGeneratedOnAdd();
			builder.HasOne(x => x.User).WithMany(x => x.UserConnections).HasForeignKey(x => x.UserId);
			builder.Property(x => x.ConnectedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETUTCDATE()");

			builder.ToTable("Connections");
		}
	}
}
