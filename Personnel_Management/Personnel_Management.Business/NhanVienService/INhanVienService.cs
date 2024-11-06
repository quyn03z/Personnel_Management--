using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.NhanVienService
{
	public interface INhanVienService
	{
		NhanVien Login(string email, string password);

		string GenerateJwtToken(NhanVien user);
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien?> GetByIdAsync(int id);
        Task<NhanVien> AddAsync(NhanVien nhanVien);
    }

}
