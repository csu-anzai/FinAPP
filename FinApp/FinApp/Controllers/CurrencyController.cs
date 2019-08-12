using BLL.Services.IServices;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await currencyService.GetAllAsync();

            return currencies.Any() ? Ok(currencies) : (IActionResult)NotFound();
        }
    }
}