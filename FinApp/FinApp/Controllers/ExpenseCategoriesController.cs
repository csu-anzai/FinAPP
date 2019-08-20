using AutoMapper;
using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseCategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoriesController(IMapper mapper, IExpenseCategoryService expenseCategoryService)
        {
            _mapper = mapper;
            _expenseCategoryService = expenseCategoryService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseCategory(int id)
        {
            var expenseCategoryInDb = await _expenseCategoryService.GetExpenseCategoryAsync(id);
            if (expenseCategoryInDb == null)
                return NotFound();
            await _expenseCategoryService.DeleteExpenseCategoryAsync(expenseCategoryInDb);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseCategory(int id, ExpenseCategoryDTO expenseCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var expenseCategory = await _expenseCategoryService.UpdateExpenseCategoryAsync(expenseCategoryDTO);
            if (expenseCategory == null)
                return BadRequest(new { message = "Category Id is incorrect!" });
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenseCategory()
        {
            var expenseCategories = await _expenseCategoryService.GetAllExpenseCategoryAsync();
            if (expenseCategories == null)
                return NoContent();
            return Ok(expenseCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseCategory(int id)
        {
            var expenseCategory = await _expenseCategoryService.GetExpenseCategoryAsync(id);
            if (expenseCategory == null)
                return NotFound();
            var expenseCategoryDTO = _mapper.Map<ExpenseCategory, ExpenseCategoryDTO>(expenseCategory);
            return Ok(expenseCategoryDTO);
        }

        [HttpPost("createExpenseCategory")]
        public async Task<IActionResult> CreateExpenseCategory(ExpenseCategoryDTO expenseCategoryDTO)
        {
            var expenseCategory = _mapper.Map<ExpenseCategory>(expenseCategoryDTO);
            var newExpenseCategory = await _expenseCategoryService.CreateExpenseCategoryAsync(expenseCategory);
            if (newExpenseCategory == null)
                return BadRequest(new { message = "Category is already exist!" });
            return Ok();
        }
    }
}