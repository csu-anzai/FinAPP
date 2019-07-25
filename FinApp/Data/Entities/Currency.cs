using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Entities
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
