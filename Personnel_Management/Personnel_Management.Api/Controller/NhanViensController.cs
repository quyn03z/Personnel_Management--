using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;

        public NhanViensController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nhanViens = await _nhanVienService.GetAllAsync();
            return Ok(nhanViens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nhanVien = await _nhanVienService.GetByIdAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return Ok(nhanVien);
        }

        //Thêm các action khác cho POST, PUT, DELETE
        [HttpPost]
        public async Task<IActionResult> Create(NhanVien nhanVien)
        {
            var createdNhanVien = await _nhanVienService.AddAsync(nhanVien);
            return CreatedAtAction(nameof(GetById), new { id = createdNhanVien.NhanVienId }, createdNhanVien);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, NhanVien nhanVien)
        //{
        //    if (id != nhanVien.NhanVienId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await _nhanVienService.UpdateAsync(nhanVien);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return StatusCode(500, $"Error updating employee: {ex.Message}");
        //    }
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _nhanVienService.DeleteAsync(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return StatusCode(500, $"Error deleting employee: {ex.Message}");
        //    }
        //}

    }
}
