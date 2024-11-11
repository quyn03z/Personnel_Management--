using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.EntityRepository;
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
        public IActionResult GetAllThuongPhat(int nhanVienId)
        {
            //get current month and year
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            //get danh sach thuong  phat cua nhan vien trong thang
            var list = _thuongPhatRepository.GetAllThuongPhat(currentMonth, currentYear, nhanVienId);
            //get tong thuong cua nhan vien trong thang
            var tongThuong = _thuongPhatRepository.GetTongThuongThang(currentMonth, currentYear, nhanVienId);
            //get tong phat cua nhan vien trong thang
            var tongPhat = _thuongPhatRepository.GetTongPhatThang(currentMonth,currentYear, nhanVienId);

            var luongCoBan = _thuongPhatRepository.GetLuongCoBan(nhanVienId).LuongCoBan;
            return Ok(new
            {
                DanhSachThuongPhat = list,
                TongThuong = tongThuong,
                TongPhat = tongPhat,
                LuongCoBan = luongCoBan
            });
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

        [HttpGet("GetThuongPhatByThuongPhatId")]
        public IActionResult GetThuongPhatByThuongPhatId(int thuongPhatId)
        {
            var thuongPhat = _thuongPhatRepository.GetThuongPhatById(thuongPhatId);
            if (thuongPhat == null)
            {
                return BadRequest();
            }
            return Ok(thuongPhat);
        }

        [HttpPost("AddThuongPhat")]
        public IActionResult AddThuongPhat(ThuongPhatAddModel thuongPhatAdd, int nhanVienId)
        {
            var result = _thuongPhatRepository.AddThuongPhat(thuongPhatAdd, nhanVienId);
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
