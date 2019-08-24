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
    public class IncomeCategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public IncomeCategoriesController(IMapper mapper, IIncomeCategoryService incomeCategoryService)
        {
            _mapper = mapper;
            _incomeCategoryService = incomeCategoryService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeCategory(int id)
        {
            var incomeCategoryInDb = await _incomeCategoryService.GetIncomeCategoryAsync(id);
            if (incomeCategoryInDb == null)
                return NotFound();
            await _incomeCategoryService.DeleteIncomeCategoryAsync(incomeCategoryInDb);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncomeCategory(int id, IncomeCategoryDTO incomeCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var incomeCategory = await _incomeCategoryService.UpdateIncomeCategoryAsync(incomeCategoryDTO);
            if (incomeCategory == null)
                return BadRequest(new { message = "Category Id is incorrect!" });
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomeCategory()
        {
            var incomeCategories = await _incomeCategoryService.GetAllIncomeCategoryAsync();
            if (incomeCategories == null)
                return NoContent();
            return Ok(incomeCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeCategory(int id)
        {
            var incomeCategory = await _incomeCategoryService.GetIncomeCategoryAsync(id);
            if (incomeCategory == null)
                return NotFound();
            var incomeCategoryDTO = _mapper.Map<IncomeCategory, IncomeCategoryDTO>(incomeCategory);
            return Ok(incomeCategoryDTO);
        }

        [HttpPost("createIncomeCategory")]
        public async Task<IActionResult> CreateIncomeCategory(IncomeCategoryDTO incomeCategoryDTO)
        {
            var incomeCategory = _mapper.Map<IncomeCategory>(incomeCategoryDTO);
            var newIncomeCategory = await _incomeCategoryService.CreateIncomeCategoryAsync(incomeCategory);
            if (newIncomeCategory == null)
                return BadRequest(new { message = "Category is already exist!" });
            return Ok();
        }
    }
}