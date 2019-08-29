using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : Controller
    {
        private readonly IIncomeService incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            this.incomeService = incomeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddIncomeAsync(IncomeAddViewModel incomeModel)
        {
            var result = await incomeService.AddIncomeAsync(incomeModel);
            return Ok(new { message = "Adding income was successful" });
        }
    }
}