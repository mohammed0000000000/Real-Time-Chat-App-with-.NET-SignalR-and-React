namespace SignlR_Web_ApI.Models
{
	public class Message
	{
		public int MessageId { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

		public string userId { get; set; } = string.Empty;
		public User User { get; set; }


        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }

        public bool ?isPrivate { get; set; }
    }
}
