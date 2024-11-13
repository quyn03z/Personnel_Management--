using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.AuthService
{
	public class AuthService : IAuthService
	{
		private readonly INhanVienRepository _nhanVienRepository;
		private readonly string _jwtSecretKey;
		private readonly string _jwtIssuer = "https://localhost:7102";
		private readonly string _jwtAudience = "https://localhost:7102";

		public AuthService(INhanVienRepository userRepository, IConfiguration configuration)
		{
			_nhanVienRepository = userRepository;
			_jwtSecretKey = configuration["Jwt:Key"];
		}

		public string GenerateJwtToken(NhanVien user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "User cannot be null.");
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
			new Claim(JwtRegisteredClaimNames.Name, user.HoTen),
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim("NhanVienId", user.NhanVienId.ToString()),
			new Claim("PhongBanId", user.PhongBanId.ToString()),
			new Claim("RoleId", user.RoleId.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.Role, user.RoleId switch
				{
				1 => "Admin",
				2 => "Manager",
				3 => "Employee",
				_ => "Unknown"
				})
				}),
				Expires = DateTime.UtcNow.AddMinutes(20),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
				Audience = _jwtAudience,
				Issuer = _jwtIssuer
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}


		public NhanVien Login(string email, string matkhau)
		{
			Console.WriteLine($"Attempting to login with Email: {email}");

			var user = _nhanVienRepository.Login(email, matkhau);

			if (user == null)
			{
				Console.WriteLine("Login failed: User not found.");
				return null;
			}

			return user; 
		}





	}
}
