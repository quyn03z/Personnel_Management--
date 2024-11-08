using Microsoft.AspNetCore.Mvc;

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
        var department = await _departmentService.GetDepartmentByIdAsync(id);
        if (department == null)
            return NotFound();

        return Ok(department);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
    {
        await _departmentService.AddDepartmentAsync(departmentDto);
        return Ok("Department added successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
    {
        try
        {
            await _departmentService.UpdateDepartmentAsync(id, departmentDto);
            return Ok("Cập nhật thành công.");
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
            return Ok("Department deleted.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}