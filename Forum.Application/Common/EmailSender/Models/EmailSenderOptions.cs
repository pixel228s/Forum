namespace Forum.Application.Common.EmailSender.Models
{
    public class EmailSenderOptions
    {
        public const string Key = "EmailSender";
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string SenderAddress { get; set; }
        public required string SenderName { get; set; }
        public required string SmtpServer { get; set; }
        public required int Port { get; set; }
    }
}
