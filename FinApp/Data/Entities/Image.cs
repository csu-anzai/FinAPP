using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }
        public ICollection<IncomeCategory> IncomeCategories { get; set; }

        public Image()
        {
            Accounts = new Collection<Account>();
            ExpenseCategories = new Collection<ExpenseCategory>();
            IncomeCategories = new Collection<IncomeCategory>();
        }
    }
}
