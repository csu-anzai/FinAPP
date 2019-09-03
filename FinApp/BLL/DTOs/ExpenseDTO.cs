namespace BLL.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public ExpenseCategoryDTO ExpenseCategory { get; set; }

        public TransactionDTO Transaction { get; set; }
    }
}
