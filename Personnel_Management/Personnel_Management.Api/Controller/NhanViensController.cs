using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.DTO;
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
                    if (newManager == null)
                        return NotFound("New manager not found.");

                    if (newManager.PhongBanId != employee.PhongBanId)
                        return BadRequest("The new manager must be in the same department.");

                    newManager.RoleId = 2; // Assign as manager
                    _context.NhanViens.Update(newManager);
                }
            

            _context.NhanViens.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
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


		[HttpPut("updateProfileEmployee/{id}")]
		public async Task<ActionResult<NhanVienDtto>> UpdateProfileEmployee(int id, [FromBody] NhanVienDtto nhanVienDTO)
		{
			var nhanVien = await _nhanVienService.GetByIdAsync(id);
			if (nhanVien == null)
			{
				return NotFound(new { message = "NhanVien not found" });
			}

			try
			{
				var updatedNhanVienDTO = await _nhanVienService.UpdateProfileEmployee(id, nhanVienDTO);
				return Ok(updatedNhanVienDTO);
			}
			catch (DbUpdateException dbEx)
			{
				if (dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
				{
					return BadRequest(new { message = "Email đã tồn tại trong hệ thống. Vui lòng chọn email khác." });
				}

				return StatusCode(500, new { message = "Đã xảy ra lỗi khi cập nhật thông tin nhân viên." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Đã xảy ra lỗi không xác định.", details = ex.Message });
			}
		}




		[HttpGet("GetById/{id}")]
		public async Task<ActionResult<NhanVienDtto>> GetCustomer(int id)
		{
			var nhanVien = await _nhanVienService.GetNhanVienById(id);
			if (nhanVien == null)
			{
				return NotFound(new { message = "NhanVien not found" });
			}

			return Ok(new { NhanVienDTO = nhanVien });
		}

        [HttpPut("Change-Password/{id}")]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] ChangePasswordProfileRequest request)
        {
            var nhanVien = await _nhanVienService.GetByIdAsync(id);
            if (nhanVien == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Kiểm tra mật khẩu cũ
            if (!await _nhanVienService.VerifyPasswordAsync(nhanVien, request.OldPassword))
            {
                return BadRequest(new { message = "Mật khẩu cũ không đúng." });
            }

            // Thay đổi mật khẩu
            bool isPasswordChanged = await _nhanVienService.ChangePasswordAsync(id, request.NewPassword);
            if (!isPasswordChanged)
            {
                return StatusCode(500, new { message = "Failed to change password" });
            }

            return Ok(new { message = "Password changed successfully" });
        }


       

	}
	public class ChangePasswordProfileRequest
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}

}



