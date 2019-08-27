namespace BLL.Models.ViewModels
{
    public class AccountAddModel
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int CurrencyId { get; set; }

        public int UserId { get; set; }

        public int ImageId { get; set; }
    }
}
