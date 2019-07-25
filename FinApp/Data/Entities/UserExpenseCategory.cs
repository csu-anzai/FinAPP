using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class UserExpenseCategory
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
