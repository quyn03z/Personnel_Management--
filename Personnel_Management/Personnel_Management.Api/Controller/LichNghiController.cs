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
    //id
    public IActionResult UpdateLichNghiByExactDate([FromQuery] int day, [FromQuery] int month, [FromQuery] int year, [FromBody] string lichnghi)
    {
        //if (string.IsNullOrEmpty(newLyDo))
        //{
        //    return BadRequest("Lý do không hợp lệ.");
        //}
        _lichNghiRepository.UpdateLichNghiByExactDate(day, month, year, lichnghi);
        return Ok();
    }



    // Xóa lịch nghỉ theo id
    [HttpDelete("DeleteLichNghiByExactDate")]
    public IActionResult DeleteLichNghiByExactDate([FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
    {
        _lichNghiRepository.DeleteLichNghiByExactDate(day, month, year);
        return Ok("Lịch nghỉ đã được xóa theo ngày cụ thể.");
    }

}
