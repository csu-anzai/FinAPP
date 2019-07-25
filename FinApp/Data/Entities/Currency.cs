using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ExchangeRate { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public Currency()
        {
            Accounts = new Collection<Account>();
        }
    }
}
