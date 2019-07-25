using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Entities
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<UserExpenseCategory> UserExpenseCategories { get; set; }

        public ExpenseCategory()
        {
            Expenses = new Collection<Expense>();
            UserExpenseCategories = new Collection<UserExpenseCategory>();
        }
    }
}
