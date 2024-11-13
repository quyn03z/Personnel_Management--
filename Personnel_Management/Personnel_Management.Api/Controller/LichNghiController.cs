using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.EntityRepository;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichNghiController : ControllerBase
    {
        private readonly ILichNghiRepository _lichNghiReopsitory;

        public LichNghiController(ILichNghiRepository lichNghiRepository) {
            _lichNghiReopsitory = lichNghiRepository;
        }
        [HttpGet("GetAllLichNghiOnMonth")]
        public IActionResult GetAllLichNghiOnMonth(int currentMonth, int currentYear)
        {
            var list = _lichNghiReopsitory.GetAllLichNghi(currentMonth, currentYear);
            return Ok(list);
        }
    }
}
