using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]

public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments()
    {
        var departments = await _departmentService.GetAllDepartmentsAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        if (id <= 0)
            return BadRequest("ID không hợp lệ.");

        var department = await _departmentService.GetDepartmentByIdAsync(id);
        if (department == null)
            return NotFound(new { message = "Không tìm thấy phòng ban." });

        return Ok(department);
    }

    [HttpGet("{id}/employees")]
    public async Task<IActionResult> GetEmployeesByDepartmentId(int id)
    {
        var employees = await _departmentService.GetEmployeesByDepartmentIdAsync(id);
        if (employees == null || employees.Count == 0)
            return NotFound("No employees found for this department.");

        return Ok(employees);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
    {
        await _departmentService.AddDepartmentAsync(departmentDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
    {
        try
        {
            await _departmentService.UpdateDepartmentAsync(id, departmentDto);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        try
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("TopTotalNhanVienInPhongBan")]
    public async Task<IActionResult> GetTotalNhanVienInPhongBan(int count)
    {
        try
        {
            var result = await _departmentService.GetTopTotalNhanVienInPhongBanAsync(count);
            return Ok(result); // Trả về status code 200 với dữ liệu
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return StatusCode(500, "Đã có lỗi xảy ra khi lấy số lượng nhân viên trong phòng ban."); // Trả về lỗi 500 nếu có lỗi
        }
    }
    [HttpGet("TotalNhanVienInPhongBan")]
    public async Task<IActionResult> GetTotalNhanVienInPhongBan()
    {
        try
        {
            var result = await _departmentService.GetTotalNhanVienInPhongBanAsync();
            return Ok(result); // Trả về status code 200 với dữ liệu
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return StatusCode(500, "Đã có lỗi xảy ra khi lấy số lượng nhân viên trong phòng ban."); // Trả về lỗi 500 nếu có lỗi
        }
    }
    [HttpGet("total-Department")]
    public async Task<IActionResult> GetTotalDepartment()
    {
        try
        {
            int totalDepartment = await _departmentService.GetTotalDepartmentAsync();
            return Ok(totalDepartment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


}