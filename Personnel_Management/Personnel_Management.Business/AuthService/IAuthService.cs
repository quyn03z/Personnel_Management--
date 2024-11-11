using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.AuthService
{
	public interface IAuthService
	{
		NhanVien Login(string email, string matkhau);

		string GenerateJwtToken(NhanVien user);
	}
}
