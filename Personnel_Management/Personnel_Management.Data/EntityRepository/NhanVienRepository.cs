using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public class NhanVienRepository : BaseRepository<NhanVien>, INhanVienRepository
	{
		public NhanVienRepository(QuanLyNhanSuContext context) : base(context)
		{
		}
		public NhanVien Login(string email, string matkhau)
		{
			return _context.NhanViens
			.FirstOrDefault(user => user.Email == email && user.Matkhau == matkhau);
		}

	}
}
