using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public interface INhanVienRepository
	{
        Task<List<NhanVienDto>> GetAllNhanViensAsync();
        Task<NhanVien> GetNhanVienByIdAsync(int id);
        Task AddNhanVienAsync(NhanVien nhanVien);
        Task UpdateNhanVienAsync(NhanVien nhanVien);
        IQueryable<NhanVien> GetQuery();
        IQueryable<NhanVien> GetQuery(Expression<Func<NhanVien, bool>> predicate);
        List<NhanVien> GetAllManagerFunction(int phongBanId);
		NhanVien GetByIdManagerFunction(int id, int phongBanId);


		NhanVien Login(string email, string matkhau);
		string HashPassword(string matkhau);

		Task<NhanVien> GetById(int id);

		Task<NhanVien> Update(NhanVien nhanVien);

	}
}
