using System.Collections.Generic;

namespace BLL.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        
        public CurrencyDTO Currency { get; set; }
        
        public ImageDTO Image { get; set; }

        public ICollection<IncomeDTO> Incomes { get; set; }
        public ICollection<ExpenseDTO> Expenses { get; set; }
    }
}
