using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Repo.EntityFrameWork.Data.Configuration
{
	public class PrivateMessageConfig : IEntityTypeConfiguration<PrivateMessage>
	{
		public void Configure(EntityTypeBuilder<PrivateMessage> builder) {
			builder.HasKey(x => x.PrivateMessageId);

			builder.Property(x => x.PrivateMessageId)
				.ValueGeneratedOnAdd();

			builder.Property(x => x.SenderId)
				.IsRequired(false); // SenderId is required

			builder.Property(x => x.ReceiverId)
				.IsRequired(false); // ReceiverId is required

			builder.Property(x => x.MessageText)
				.IsRequired()
				.HasMaxLength(256)
				.HasColumnType("VARCHAR");

			builder.Property(x => x.SentAt)
				.HasColumnType("DATETIME") // Store both date and time
				.HasDefaultValueSql("GETUTCDATE()");

			builder.HasOne(x => x.Sender)
				.WithMany(x => x.SentPrivateMessages)
				.HasForeignKey(x => x.SenderId)
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

			builder.HasOne(x => x.Receiver)
				.WithMany(x => x.ReceivedPrivateMessages) // Fixed typo
				.HasForeignKey(x => x.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

			builder.ToTable("PrivateMessages");
		}
	}
}
