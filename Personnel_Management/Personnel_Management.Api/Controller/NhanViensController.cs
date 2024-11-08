using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;
        private readonly QuanLyNhanSuContext _context;
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanViensController(INhanVienService nhanVienService, QuanLyNhanSuContext context, INhanVienRepository nhanVienRepository)
        {
            _nhanVienService = nhanVienService;
            _context = context;
            _nhanVienRepository = nhanVienRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nhanViens = await _nhanVienService.GetAllAsync();
            return Ok(nhanViens );
        }

        [HttpGet("GetAllManagerFunction")]
        public  IActionResult GetAllManagerFunction()
        {
            var nhanViens = _nhanVienRepository.GetAllManagerFunction();
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

        [HttpGet("GetByIdManagerFunction")]
        public IActionResult GetByIdManagerFunction(int id)
        {
            var nhanVien =  _nhanVienRepository.GetByIdManagerFunction(id);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.NhanVienId)
            {
                return BadRequest();
            }

            try
            {
                await _nhanVienService.UpdateAsync(nhanVien);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Error updating employee: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, [FromQuery] int? newManagerId = null)
        {
            var employee = await _context.NhanViens.FindAsync(id);
            if (employee == null)
                return NotFound("Employee not found.");

            if (employee.IsManager && newManagerId == null)
                return BadRequest("Must assign a new manager.");

            if (employee.IsManager)
            {
                var newManager = await _context.NhanViens.FindAsync(newManagerId);
                newManager.RoleId = 2; // Assign as manager
                _context.NhanViens.Update(newManager);
            }

            _context.NhanViens.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok("Employee deleted.");
        }


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
