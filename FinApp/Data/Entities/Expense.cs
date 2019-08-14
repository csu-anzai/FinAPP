using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ExpenseCategory ExpenseCategory { get; set; }

        public int TransactionId { get; set; }

        [ForeignKey(nameof(TransactionId))]
        public Transaction Transaction { get; set; }

    }
}