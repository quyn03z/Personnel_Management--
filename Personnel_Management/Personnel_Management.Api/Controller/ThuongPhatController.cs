using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.ThuongPhatRepository;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongPhatController : ControllerBase
    {
        private readonly IThuongPhatRepository _thuongPhatRepository;

        public ThuongPhatController(IThuongPhatRepository thuongPhatRepository)
        {
            _thuongPhatRepository = thuongPhatRepository;
        }

        [HttpGet("GetAllThuongPhat")]
        public IActionResult GetAllThuongPhat(string fromDate, string toDate)
        {
            var list = _thuongPhatRepository.GetAllThuongPhat(fromDate, toDate);
            if (list == null)
            {
                return BadRequest();
            }
            return Ok(list);
        }

        [HttpGet("GetThuongPhatByNhanVienId")]
        public IActionResult GetThuongPhatByNhanVienId(int nhanVienId)
        {
            var list = _thuongPhatRepository.GetThuongPhatByNhanVienId(nhanVienId);
            if (list == null)
            {
                return BadRequest();
            }
            return Ok(list);
        }

        [HttpPost("AddPhat")]
        public IActionResult AddPhat(ThuongPhatAddModel phatAdd)
        {
            var result = _thuongPhatRepository.AddPhat(phatAdd);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost("AddThuong")]
        public IActionResult AddThuong(ThuongPhatAddModel thuongAdd)
        {
            var result = _thuongPhatRepository.AddThuong(thuongAdd);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("UpdateThuongPhat")]
        public IActionResult UpdateThuongPhat(int thuongPhatId, ThuongPhatAddModel thuongPhatUpdate)
        {
            var result = _thuongPhatRepository.UpdateThuongPhat(thuongPhatId, thuongPhatUpdate);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteThuongPhatById")]
        public IActionResult DeleteThuongPhatById(int thuongPhatId)
        {
            var result = _thuongPhatRepository.DeleteThuongPhatById(thuongPhatId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
