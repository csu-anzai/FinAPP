namespace DAL.Entities
{
    public class UserIncomeCategory
    {
            public int UserId { get; set; }
            public User User { get; set; }

            public int IncomeCategoryId { get; set; }
            public IncomeCategory IncomeCategory { get; set; }
    }
}
