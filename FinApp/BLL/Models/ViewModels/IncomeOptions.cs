using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.ViewModels
{
    public class TransactionOptions
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime SecondDate { get; set; }
    }
}
