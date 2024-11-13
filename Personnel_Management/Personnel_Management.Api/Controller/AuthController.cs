using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.AuthService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Models.Models;
using Personnel_Management.Models.ModelsDTO;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Personnel_Management.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
				return BadRequest("Tài khoản không tồn tại.");
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



		//[HttpPost]
		//[Route("ForgetPassword")]
		//public async Task<IActionResult> ForgetPassword([FromBody] EmailRequest request)
		//{
		//	var email = request.Email;

		//	var nhanVienExists = _quanLyNhanSuContext.NhanViens.Any(x => x.Email == email);

		//	if (!nhanVienExists)
		//	{
		//		return NotFound(new { Message = "Email does not exist" });
		//	}

		//	// Generate OTP
		//	Random random = new Random();
		//	string otp = random.Next(100000, 999999).ToString();

		//	// Send OTP to the user's email
		//	SendOtpEmail(email, otp);


		//	// Create a new OtpEntry and store it in session
		//	var otpEntry = new OtpEntry { Email = email, Otp = otp };
		//	HttpContext.Session.SetString("otpEntry", Newtonsoft.Json.JsonConvert.SerializeObject(otpEntry));

		//	return Ok(new { Message = "OTP has been sent to your email" });

		//}



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

			Random random = new Random();
			string otp = random.Next(100000, 999999).ToString();

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("email", email),
					new Claim("otp", otp)
				}),
				Expires = DateTime.UtcNow.AddMinutes(5),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = tokenHandler.WriteToken(token);

			SendOtpEmail(email, otp);

			return Ok(new { Message = "OTP has been sent to your email", Token = jwtToken });
		}


		[HttpPost("VerifyOtp")]
		public IActionResult VerifyOtp([FromBody] VerifyOtpRequest request)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			try
			{
				tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				var email = jwtToken.Claims.First(x => x.Type == "email").Value;
				var otp = jwtToken.Claims.First(x => x.Type == "otp").Value;

				if (email == request.Email && otp == request.Otp)
				{
					// Generate a new password reset token
					var passwordResetToken = Guid.NewGuid().ToString();
					_resetTokens.Add(new ResetTokenEntry { Email = email, Token = passwordResetToken });

					return Ok(new { Message = "OTP verified successfully.", PasswordResetToken = passwordResetToken });
				}
				else
				{
					return BadRequest(new { Message = "Invalid OTP or email." });
				}
			}
			catch
			{
				return Unauthorized(new { Message = "Invalid or expired token." });
			}
		}





		[HttpPost("ChangePassword")]
		public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
		{
			// Find the reset token entry
			var resetTokenEntry = _resetTokens.FirstOrDefault(rt => rt.Token == request.PasswordResetToken && rt.Email == request.Email);

			if (resetTokenEntry == null)
			{
				return Unauthorized(new { Message = "Invalid or expired password reset token." });
			}

			// Find the user by email
			var nhanVien = _quanLyNhanSuContext.NhanViens.FirstOrDefault(x => x.Email == request.Email);
			if (nhanVien == null)
			{
				return NotFound(new { Message = "User not found" });
			}

			// Hash the new password and update the user's password
			var hashedPassword = HashPassword(request.NewPassword);
			nhanVien.Matkhau = hashedPassword;

			// Save changes and remove the reset token
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
				var fromAddress = new MailAddress(_configuration["Smtp:Username"], "Personnel-Management");
				var toAddress = new MailAddress(recipientEmail);
				string fromPassword = _configuration["Smtp:Password"];
				string subject = "Your OTP Code";
				string body = $"Your OTP code is: {otp}.";

				// Set up SMTP client settings
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

	public class VerifyOtpRequest
	{
		public string Email { get; set; }
		public string Otp { get; set; }
		public string Token { get; set; }
	}

	public class ChangePasswordRequest
	{
		public string Email { get; set; }
		public string PasswordResetToken { get; set; }
		public string NewPassword { get; set; }
	}



}
