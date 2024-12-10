namespace SignlR_Web_ApI.Models
{
	public class PrivateMessage
	{
        public int PrivateMessageId { get; set; }

        public string SenderId { get; set; } = string.Empty;
        public User Sender { get; set; }

        public string ReceiverId { get; set; } = string.Empty;
        public User Receiver { get; set; }


        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}
