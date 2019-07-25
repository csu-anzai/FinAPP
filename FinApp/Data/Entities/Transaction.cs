using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }

        public Income Income { get; set; }
        public Expense Expense { get; set; }
    }
}
