using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.LuongService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuongController : ControllerBase
    {
        private readonly ILuongService _luongService;
        private readonly INhanVienService _nhanVienService;


        private readonly QuanLyNhanSuContext _context;
        private readonly IDiemDanhRepository _diemDanhRepository;
        private readonly IThuongPhatRepository _thuongPhatReopository;
        private readonly ILuongRepository _luongRepository;

        public LuongController(ILuongService luongService, INhanVienService nhanVienService , QuanLyNhanSuContext context, IDiemDanhRepository diemDanhRepository, IThuongPhatRepository thuongPhatRepository, ILuongRepository luongRepository)
        {
            _nhanVienService = nhanVienService;
            _luongService = luongService;
            _context = context;
            _diemDanhRepository = diemDanhRepository;
            _thuongPhatReopository = thuongPhatRepository;
            _luongRepository = luongRepository;
        }

        [HttpPost("{nhanVienId}")]
        public async Task<IActionResult> AddLuongForNhanVien(int nhanVienId, [FromBody] Luong luong)
        {
            // Kiểm tra xem nhân viên tồn tại không
            var nhanVien = await _nhanVienService.GetByIdAsync(nhanVienId);
            if (nhanVien == null)
            {
                return NotFound($"Nhân viên với ID {nhanVienId} không tồn tại.");
            }

            // Kiểm tra xem đã tồn tại lương cho nhân viên này chưa.  Bạn có thể thêm logic phức tạp hơn ở đây nếu cần.
            if (nhanVien.Luongs.Any())
            {
                return BadRequest($"Nhân viên với ID {nhanVienId} đã có lương.");
            }

            // Gán nhân viên cho lương
            luong.NhanVienId = nhanVienId;

            try
            {
                await _luongService.AddAsync(luong);
                return Ok(luong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi thêm lương: {ex.Message}");
            }
        }

        [HttpGet("GetSalaryEmployee")]
        public IActionResult GetSalaryEmployee(int nhanVienId) {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var luongCoBan = _luongRepository.GetLuongCoBanByNhanVienId(nhanVienId);
            var ngayCongChuan = _diemDanhRepository.GetNgayCongChuan(currentMonth, currentYear);
            
            var soNgayDiLam = _diemDanhRepository.GetNumberOfDaysWorked(currentMonth, currentYear, nhanVienId);

            var tongThuong = _thuongPhatReopository.GetTongThuongThang(currentMonth, currentYear, nhanVienId);
            var tongPhat = _thuongPhatReopository.GetTongPhatThang(currentMonth,currentYear, nhanVienId);

            decimal luongThucTeDecimal = ((luongCoBan / ngayCongChuan) * soNgayDiLam) + tongThuong - tongPhat;

            decimal luongThucTe = Math.Round(luongThucTeDecimal, 0, MidpointRounding.AwayFromZero);


            return Ok(new
            {
                LuongCoBan = luongCoBan,
                NgayCongChuan = ngayCongChuan,
                SoNgayDiLam = soNgayDiLam,
                TongThuong = tongThuong,
                tongPhat = tongPhat,
                LuongThucTe = luongThucTe
            });
        }
    }

}

