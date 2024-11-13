using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.EntityRepository;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemDanhController : ControllerBase
    {
        private readonly IDiemDanhRepository _diemDanhRepository;

        public DiemDanhController(IDiemDanhRepository diemDanhRepository)
        {
            _diemDanhRepository = diemDanhRepository;
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
    }
}
