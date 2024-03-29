﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Avatar { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }

        public Token Token { get; set; }

        public PasswordConfirmationCode PasswordConfirmationCode { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<UserIncomeCategory> UserIncomeCategories { get; set; }
        public ICollection<UserExpenseCategory> UserExpenseCategories { get; set; }

        public User()
        {
            Accounts = new Collection<Account>();
            UserIncomeCategories = new Collection<UserIncomeCategory>();
            UserExpenseCategories = new Collection<UserExpenseCategory>();
        }
    }
}
