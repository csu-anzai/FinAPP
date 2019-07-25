using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Income> Incomes { get; set; }
        public ICollection<Expense> Expenses { get; set; }

        public Account()
        {
            Incomes = new Collection<Income>();
            Expenses = new Collection<Expense>();
        }
    }
}
