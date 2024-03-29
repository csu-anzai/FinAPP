﻿using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("{userId}/{accountId}")]
        public async Task<IActionResult> Get(int userId, int accountId)
        {
            var account = await accountService.GetInfoById(userId, accountId);

            return account != null ? Ok(account) : (IActionResult)NotFound();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAccount(AccountAddModel account)
        {
            var result = await accountService.AddAccount(account);

            return result == null ? Ok(new { message = "Adding account was successful" }) : (IActionResult)Conflict(new { message = "User already has account with this name" });
        }
    }
}