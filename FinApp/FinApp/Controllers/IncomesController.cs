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
        [HttpPost("add")]
        public async Task<IActionResult> AddIncome()
        {
            return Ok(new { message = "Adding income was successful" });
        }

    }
}