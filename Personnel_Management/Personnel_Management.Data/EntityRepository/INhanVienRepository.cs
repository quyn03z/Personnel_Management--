using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public interface INhanVienRepository : IBaseRepository<NhanVien>
	{
		NhanVien Login(string email, string matkhau);
	}

}
