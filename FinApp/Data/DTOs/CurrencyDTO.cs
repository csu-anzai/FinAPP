using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
