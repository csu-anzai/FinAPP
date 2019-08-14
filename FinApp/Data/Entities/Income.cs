using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Income
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public IncomeCategory IncomeCategory { get; set; }

        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
