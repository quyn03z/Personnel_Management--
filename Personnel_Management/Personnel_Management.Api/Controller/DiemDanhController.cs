using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Business.DiemDanhService;
using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemDanhController : ControllerBase
    {
        private readonly IDiemDanhRepository _diemDanhRepository;
		private readonly IDiemDanhService _diemDanhService;

        public DiemDanhController(IDiemDanhRepository diemDanhRepository, IDiemDanhService diemDanhService)
        {
            _diemDanhRepository = diemDanhRepository;
			_diemDanhService = diemDanhService;
        }
        [HttpGet("GetAllDiemDanhById")]
        public IActionResult GetAllDiemDanhById(int nhanVienId)
        {
            //get current month and year
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            // lay ra danh sach diem danh trong thang
            var danhSachDiemDanh = _diemDanhRepository.GetAllDiemDanhById(currentMonth, currentYear, nhanVienId);
            //l
            var soNgayDiLam = _diemDanhRepository.GetNumberOfDaysWorked(currentMonth, currentYear, nhanVienId);

            return Ok(new
            {
                DanhSachDiemDanh = danhSachDiemDanh,
                SoNgayDiLam = soNgayDiLam
            });
        }
		[HttpGet("GetDiemDanhByNhanVienId/{id}")]
		public async Task<ActionResult<IEnumerable<DiemDanh>>> GetDiemDanhByNhanVienId(int id, int thang, int nam)
		{
			var diemDanhList = await _diemDanhService.GetAllDiemDanhNhanVienByIdAsync(id,thang,nam);

			if (diemDanhList == null || !diemDanhList.Any())
			{
				return NotFound();
			}

			return Ok(diemDanhList);
		}


		[HttpPost("DiemDanhCoNhanVien")]
		public async Task<ActionResult<DiemDanh>> DiemDanhCoNhanVien([FromBody] DiemDanhDTO diemDanh)
		{
			if (diemDanh == null)
			{
				return BadRequest(new { message = "Invalid attendance data." });
			}

			try
			{
				var addedDiemDanh = await _diemDanhService.AddDiemDanhCoAsync(diemDanh);
				return Ok(addedDiemDanh);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An error occurred while adding attendance record.", details = ex.Message });
			}
		}


		[HttpPost("DiemDanhVangNhanVien")]
		public async Task<ActionResult<DiemDanh>> DiemDanhVangNhanVien([FromBody] DiemDanhDTO diemDanh)
		{
			if (diemDanh == null)
			{
				return BadRequest(new { message = "Invalid attendance data." });
			}

			try
			{
				var addedDiemDanh = await _diemDanhService.AddDiemDanhVangAsync(diemDanh);
				return Ok(addedDiemDanh);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An error occurred while adding attendance record.", details = ex.Message });
			}
		}
    }
}
