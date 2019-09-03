using BLL.DTOs;

namespace BLL.Models.ViewModels
{
    public class IncomeUpdateViewModel
    {
        public int Id { get; set; }

        public int IncomeCategoryId { get; set; }
        
        public TransactionDTO Transaction { get; set; }
    }
}
