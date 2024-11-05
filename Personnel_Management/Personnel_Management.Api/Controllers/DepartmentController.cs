// Controllers/DepartmentController.cs
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Models.Models;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddDepartment([FromBody] PhongBan department)
    {
        _context.PhongBans.Add(department);
        await _context.SaveChangesAsync();
        return Ok(department);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.PhongBans
                            .Include(d => d.Employees)
                            .FirstOrDefaultAsync(d => d.PhongBanId == id);

        if (department == null)
            return NotFound("Department not found.");

        if (department.Employees.Any())
            return BadRequest("Cannot delete department with employees.");

        _context.PhongBans.Remove(department);
        await _context.SaveChangesAsync();
        return Ok("Department deleted.");
    }
}
