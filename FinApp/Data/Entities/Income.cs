using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Income
    {
        public int Id { get; set; }


        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int ExpenceCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }

        public int TransactionId { get; set; }
        [ForeignKey(nameof(TransactionId))]
        public Transaction Transaction { get; set; }
    }
}
