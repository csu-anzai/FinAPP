namespace BLL.DTOs
{
    public class IncomeDTO
    {
        public int Id { get; set; }

        public IncomeCategoryDTO IncomeCategory { get; set; }

        public TransactionDTO Transaction { get; set; }
    }
}
