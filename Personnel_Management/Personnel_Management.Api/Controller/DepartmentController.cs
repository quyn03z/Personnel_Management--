// Controllers/DepartmentController.cs
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Models.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly QuanLyNhanSuContext _context;

    public DepartmentController(QuanLyNhanSuContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments()
    {
        try
        {
            var departments = await _context.PhongBans.ToListAsync();
            return Ok(departments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        try
        {
            var department = await _context.PhongBans.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddDepartment([FromBody] PhongBan department)
    {
        _context.PhongBans.Add(department);
        await _context.SaveChangesAsync();
        return Ok(department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] PhongBan department)
    {
        if (id != department.PhongBanId)
        {
            return BadRequest("ID mismatch.");
        }

        try
        {
            var existingDepartment = await _context.PhongBans.FindAsync(id);
            if (existingDepartment == null)
            {
                return NotFound("Phòng ban không tồn tại.");
            }

            existingDepartment.TenPhongBan = department.TenPhongBan;
            existingDepartment.MoTa = department.MoTa;

            // Cập nhật các thuộc tính khác nếu cần
            _context.PhongBans.Update(existingDepartment);
            await _context.SaveChangesAsync();

            return Ok("Cập nhật thành công."); // Hoặc bạn có thể trả lại dữ liệu cập nhật nếu muốn
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.PhongBans
                            .Include(d => d.NhanViens)
                            .FirstOrDefaultAsync(d => d.PhongBanId == id);

        if (department == null)
            return NotFound("Department not found.");

        if (department.NhanViens.Any())
            return BadRequest("Cannot delete department with employees.");

        _context.PhongBans.Remove(department);
        await _context.SaveChangesAsync();
        return Ok("Department deleted.");
    }
}
