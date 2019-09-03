namespace BLL.Models.ViewModels
{
    public class IncomeAddViewModel
    {
        public int AccountId { get; set; }

        public int IncomeCategoryId { get; set; }
        
        public TransactionViewModel Transaction { get; set; }
    }
}
