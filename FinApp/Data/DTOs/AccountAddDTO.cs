﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class AccountAddDTO
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int CurrencyId { get; set; }

        public int UserId { get; set; }

        public int ImageId { get; set; }
    }
}
