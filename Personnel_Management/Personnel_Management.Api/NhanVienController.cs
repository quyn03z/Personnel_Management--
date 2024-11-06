using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.EntityRepository;

namespace Personnel_Management.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanVienController(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var listNhanVien = _nhanVienRepository.GetAllManagerFunction();
            if (listNhanVien == null)
            {
                return BadRequest();
            }
            return Ok(listNhanVien);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var nhanVien = _nhanVienRepository.GetByIdManagerFunction(id);
            if (nhanVien == null)
            {
                return BadRequest();
            }
            return Ok(nhanVien);
        }
    }
}
