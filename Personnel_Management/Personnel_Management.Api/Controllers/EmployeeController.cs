// Controllers/EmployeeController.cs
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Models.Models;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly QuanLyNhanSuContext _context;

    public EmployeeController(QuanLyNhanSuContext context)
    {
        _context = context;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddEmployee([FromBody] NhanVien employee)
    {
        var department = await _context.PhongBans.FindAsync(employee.PhongBanId);

        if (employee.IsManager && department.NhanViens.Any(e => e.IsManager))
            return BadRequest("Department already has a manager.");

        _context.NhanViens.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(employee);
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
}
