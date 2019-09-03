using BLL.DTOs;

namespace BLL.Models.ViewModels
{
    public class ExpenseUpdateViewModel
    {
        public int Id { get; set; }

        public int ExpenseCategoryId { get; set; }
        
        public TransactionDTO Transaction { get; set; }
    }
}
