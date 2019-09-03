
namespace BLL.DTOs
{
    public class EmailOptionsDTO
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string ContactEmail { get; set; }
    }
}
