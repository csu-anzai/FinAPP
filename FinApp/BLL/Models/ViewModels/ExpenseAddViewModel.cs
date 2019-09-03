namespace BLL.Models.ViewModels
{
    public class ExpenseAddViewModel
    {
        public int AccountId { get; set; }

        public int ExpenseCategoryId { get; set; }
        
        public TransactionViewModel Transaction { get; set; }
    }
}
