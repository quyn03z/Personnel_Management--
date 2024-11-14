using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;

[Route("api/[controller]")]
[ApiController]
public class LichNghiController : ControllerBase
{
    private readonly ILichNghiRepository _lichNghiRepository;

    public LichNghiController(ILichNghiRepository lichNghiRepository)
    {
        _lichNghiRepository = lichNghiRepository;
    }

    // Lấy tất cả lịch nghỉ trong tháng với ngày và lý do
    [HttpGet("GetAllLichNghiOnMonth")]
    public IActionResult GetAllLichNghi()
    {
        var list = _lichNghiRepository.GetAllLichNghi();
        return Ok(list);
    }

    // Thêm lịch nghỉ với ngày và lý do
    [HttpPost("AddLichNghi")]
    public IActionResult AddLichNghi([FromBody] LichNghiInput model)
    {
        if (model == null || model.Ngay == default(DateTime) || string.IsNullOrEmpty(model.LyDo))
        {
            return BadRequest("Ngày và lý do không hợp lệ.");
        }

        var lichNghi = new LichNghi
        {
            Ngay = model.Ngay,
            LyDo = model.LyDo
        };

        _lichNghiRepository.AddLichNghi(lichNghi);
        return Ok("Lịch nghỉ đã được thêm thành công.");
    }

    [HttpPut("UpdateLichNghiByExactDate")]

    //public IActionResult UpdateLichNghiByExactDate([FromQuery] int day, [FromQuery] int month, [FromQuery] int year, [FromBody] string lichnghi)

    public IActionResult UpdateLichNghiByExactDate([FromQuery] int day, [FromQuery] int month, [FromQuery] int year, [FromBody] LichNghi lichnghi)

    {
        // Tìm kiếm LichNghi theo ngày chính xác
        var existingLichNghi = _lichNghiRepository.searchLichNghiByExactDate(day, month, year);
        if (existingLichNghi == null)
        {
            lichnghi.Ngay = new DateTime(year, month, day); // Tạo ngày mới cho đối tượng lichnghi
            _lichNghiRepository.AddLichNghi(lichnghi);
            return Ok(existingLichNghi);
        }
        else
        {
            try
            {
                existingLichNghi.LyDo = lichnghi.LyDo;
                _lichNghiRepository.UpdateLichNghiByExactDate(existingLichNghi);
                return Ok(existingLichNghi); // Trả về đối tượng đã được cập nhật
            }
            catch (Exception ex)
            {
                // Log the exception (có thể thêm chi tiết về lỗi vào log)
                return StatusCode(500, $"Error updating: {ex.Message}");
            }
        }
    }




    // Xóa lịch nghỉ theo id
    [HttpDelete("DeleteLichNghiByExactDate")]
    public IActionResult DeleteLichNghiByExactDate([FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
    {
        _lichNghiRepository.DeleteLichNghiByExactDate(day, month, year);
        return Ok("Lịch nghỉ đã được xóa theo ngày cụ thể.");
    }

}
