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
    public class IncomesController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            this._incomeService = incomeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddIncomeAsync(IncomeAddViewModel incomeModel)
        {
            var result = await _incomeService.AddIncomeAsync(incomeModel);
            return Ok(new { message = "Adding income was successful" });
        }

        [HttpGet()]
        public async Task<IActionResult> GetIncomesByCondition([FromQuery]TransactionOptions options)
        {
            try
            {
                var result = await _incomeService.GetIncomesWithDetailsAndConditionAsync(options);

                return Ok(result);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }    
        }
    }
}