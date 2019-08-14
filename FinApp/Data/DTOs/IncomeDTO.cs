using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class IncomeDTO
    {
        public int Id { get; set; }

        public IncomeCategoryDTO IncomeCategoryDTO { get; set; }

        public TransactionDTO TransactionDTO { get; set; }
    }
}
