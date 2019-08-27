namespace BLL.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }

        public ExpenseCategoryDTO IncomeCategory { get; set; }

        public TransactionDTO Transaction { get; set; }
    }
}
