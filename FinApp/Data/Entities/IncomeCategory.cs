using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Entities
{
    public class IncomeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Income> Incomes { get; set; }
        public ICollection<UserIncomeCategory >UserIncomeCategories { get; set; }

        public IncomeCategory()
        {
            Incomes = new Collection<Income>();
            UserIncomeCategories = new Collection<UserIncomeCategory>();
        }
    }
}
