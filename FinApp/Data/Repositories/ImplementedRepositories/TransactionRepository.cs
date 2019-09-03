using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.ImplementedRepositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public void Add(Transaction tr) {
            _entities.Add(tr);
        }
    }
}
