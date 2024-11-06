using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.AuthService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Models.Models;
using Personnel_Management.Models.ModelsDTO;


namespace Personnel_Management.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly INhanVienService _nhanVienService;

		public AuthController(IAuthService authService, INhanVienService nhanVienService)
		{
			_authService = authService;
			_nhanVienService = nhanVienService;
		}

		[HttpPost]
		[Route("Login")]
		public IActionResult Login([FromBody] LoginDTO loginDto)
		{
			if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.MatKhau))
			{
				return BadRequest("Invalid login request.");
			}
			var user = _authService.Login(loginDto.Email, loginDto.MatKhau);
			if (user == null)
			{
				return Unauthorized(); 
			}
			// Tạo JWT token cho người dùng
			var token = _authService.GenerateJwtToken(user);
			var response = new
			{
				user = new
				{
					user.Email,
				},
				token
			};

			return Ok(response);
		}



		[HttpPost]
		[Route("CreateNhanVien")]

		public async Task<IActionResult> CreateNhanVien([FromBody] NhanVienDTO nhanVienDto)
		{

			var nhanVien = new NhanVien
			{
				HoTen = nhanVienDto.HoTen,
				NgaySinh = nhanVienDto.NgaySinh,
				DiaChi = nhanVienDto.DiaChi,
				SoDienThoai = nhanVienDto.SoDienThoai,
				Email = nhanVienDto.Email,
				RoleId = nhanVienDto.RoleId,
				Matkhau = nhanVienDto.Matkhau,
			};

			await _nhanVienService.AddNhanVienAsync(nhanVien);
			return Ok(nhanVien);
		}




	}
}
