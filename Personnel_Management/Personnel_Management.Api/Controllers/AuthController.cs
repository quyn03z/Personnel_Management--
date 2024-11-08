using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.AuthService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Models.Models;
using Personnel_Management.Models.ModelsDTO;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Personnel_Management.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly INhanVienService _nhanVienService;
		private readonly QuanLyNhanSuContext _quanLyNhanSuContext;
		private readonly IConfiguration _configuration;
		public static List<ResetTokenEntry> _resetTokens = new List<ResetTokenEntry>();



		// Constructor: Ensure there is only one constructor
		public AuthController(IAuthService authService, INhanVienService nhanVienService, QuanLyNhanSuContext quanLyNhanSuContext, IConfiguration configuration)
		{
			_authService = authService;
			_nhanVienService = nhanVienService;
			_quanLyNhanSuContext = quanLyNhanSuContext;
			_configuration = configuration;
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

			// Create JWT token for the user
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
			if (_quanLyNhanSuContext.NhanViens.Any(x => x.Email == nhanVienDto.Email))
			{
				return Conflict(new { Message = "Email already exists" });
			}

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

		[HttpPost]
		[Route("ForgetPassword")]
		public async Task<IActionResult> ForgetPassword([FromBody] EmailRequest request)
		{
			var email = request.Email;

			var nhanVienExists = _quanLyNhanSuContext.NhanViens.Any(x => x.Email == email);

			if (!nhanVienExists) 
			{
				return NotFound(new { Message = "Email does not exist" });
			}

			// Generate OTP
			Random random = new Random();
			string otp = random.Next(100000, 999999).ToString();

			// Send OTP to the user's email
			SendOtpEmail(email, otp);


			// Create a new OtpEntry and store it in session
			var otpEntry = new OtpEntry { Email = email, Otp = otp };
			HttpContext.Session.SetString("otpEntry", Newtonsoft.Json.JsonConvert.SerializeObject(otpEntry));

			return Ok(new { Message = "OTP has been sent to your email" });

		}


		[HttpPost("VerifyOtp")]
		public IActionResult VerifyOtp([FromBody] OtpEntry otpEntry)
		{
			string sessionOtpEntryString = HttpContext.Session.GetString("otpEntry");

			if (string.IsNullOrEmpty(sessionOtpEntryString))
			{
				return BadRequest(new { Message = "No OTP found. Please request a new OTP." });
			}

			var sessionOtpEntry = Newtonsoft.Json.JsonConvert.DeserializeObject<OtpEntry>(sessionOtpEntryString);

			if (otpEntry.Email != sessionOtpEntry.Email || otpEntry.Otp != sessionOtpEntry.Otp)
			{
				return BadRequest(new { Message = "Invalid OTP or email" });
			}

			// OTP is valid, generate a temporary token for password change
			var token = Guid.NewGuid().ToString(); // Generate a unique token

			// Store the token and email in the static list
			_resetTokens.Add(new ResetTokenEntry { Email = otpEntry.Email, Token = token });

			// Remove OTP from session
			HttpContext.Session.Remove("otpEntry");

			return Ok(new { Message = "OTP verified. Please reset your password.", Token = token });
		}


		[HttpPost("ChangePassword")]
		public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
		{
			var resetTokenEntry = _resetTokens.FirstOrDefault(rt => rt.Token == request.Token && rt.Email == request.Email);

			if(resetTokenEntry == null)
			{
				return Unauthorized(new {Message = "Invalid token"});
			}

			var nhanVien = _quanLyNhanSuContext.NhanViens.FirstOrDefault(x => x.Email == request.Email);

			if (nhanVien == null)
			{
				return NotFound(new { Message = "User not found" });
			}


			// Hash the new password
			var hashedPassword = HashPassword(request.NewPassword);
			
			if(nhanVien != null)
			{
				nhanVien.Matkhau = hashedPassword;
			}

			if (_quanLyNhanSuContext.SaveChanges() > 0)
			{
				_resetTokens.Remove(resetTokenEntry);
				return Ok(new { Message = "Password changed successfully!" });
			}
			else
			{
				return StatusCode(500, new { Message = "An error occurred while changing the password." });
			}
		}



		private void SendOtpEmail(string recipientEmail, string otp)
		{
			try
			{
				// cấu hình người gửi
				var fromAddress = new MailAddress(_configuration["Smtp:Username"], "Personnel-Management");
				var toAddress = new MailAddress(recipientEmail);
				string fromPassword = _configuration["Smtp:Password"];
				string subject = "Your OTP Code";
				string body = $"Your OTP code is: {otp}.";

				// cấu hình kết nối
				var smtp = new SmtpClient
				{
					Host = _configuration["Smtp:Host"],
					Port = int.Parse(_configuration["Smtp:Port"]),
					EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]),
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
				};

				using (var message = new MailMessage(fromAddress, toAddress)
				{
					Subject = subject,
					Body = body
				})
				{
					smtp.Send(message);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}

		private string HashPassword(string password)
		{
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				var bytes = System.Text.Encoding.UTF8.GetBytes(password);
				var hash = sha256.ComputeHash(bytes);
				return System.Convert.ToBase64String(hash);
			}
		}

	}

	public class EmailRequest
	{
		public string Email { get; set; }
	}


	public class OtpEntry
	{
		public string Email { get; set; }
		public string Otp { get; set; }
	}


	public class ResetTokenEntry
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}


	public class ChangePasswordRequest
	{
		public string Token { get; set; }
		public string Email { get; set; }
		public string NewPassword { get; set; }
	}

}
