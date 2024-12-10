using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignlR_Web_ApI.Models;
namespace SignlR_Web_ApI.Repo.EntityFrameWork.Data.Configuration
{
	public class MessageConfig : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder) {
			builder.HasKey(x => x.MessageId);
			builder.Property(x => x.MessageId).ValueGeneratedOnAdd();
			builder.Property(x => x.MessageText).IsRequired().HasMaxLength(256).HasColumnType("VARCHAR");
			builder.Property(x => x.SentAt).HasColumnType("DATETIME").HasDefaultValueSql("GETUTCDATE()");
			builder.HasOne(x => x.Group).WithMany(x => x.Messages).HasForeignKey(x => x.GroupId);

			builder.HasOne(x => x.User).WithMany(x => x.Messages).HasForeignKey(x => x.userId);

			builder.ToTable("Messages");
		}
	}
}
