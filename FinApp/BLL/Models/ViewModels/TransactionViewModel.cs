using System;

namespace BLL.Models.ViewModels
{
    public class TransactionViewModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
    }
}
