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

    // Xóa lịch nghỉ theo id
    [HttpDelete("DeleteLichNghi/{id}")]
    public IActionResult DeleteLichNghi(int id)
    {
        _lichNghiRepository.DeleteLichNghi(id);
        return Ok("Lịch nghỉ đã được xóa.");
    }
}
