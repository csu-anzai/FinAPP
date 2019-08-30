using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;



namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly IMapper mapper;
        private readonly FinAppContext _context;
        public TransactionController(FinAppContext context, IMapper mapper )
        {
            this.mapper = mapper;
            _context = context;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]TransactionDTO model)
        {
            var transaction = mapper.Map<Transaction>(model);
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
