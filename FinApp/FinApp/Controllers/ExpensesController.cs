using BLL.Models.Exceptions;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddIncomeAsync(ExpenseAddViewModel expenseModel)
        {
            var result = await _expenseService.AddExpenseAsync(expenseModel);
            return Ok(new { message = "Adding expense was successful" });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveIncome(int id)
        {
            var updatedAccount = await _expenseService.Remove(id);
            return Ok(new { message = "Removing expense was successful", account = updatedAccount });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateIncome(ExpenseUpdateViewModel expenseModel)
        {
            var result = await _expenseService.UpdateExpense(expenseModel);
            return Ok(new { message = "Updating expense was successful"});
        }

        [HttpGet()]
        public async Task<IActionResult> GetExpensesByCondition([FromQuery]TransactionOptions options)
        {
            try
            {
                var result = await _expenseService.GetExpensesWithDetailsAndConditionAsync(options);

                return Ok(result);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}