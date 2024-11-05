using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public interface INhanVienRepository
	{
        Task<List<NhanVien>> GetAllNhanViensAsync();
        Task<NhanVien> GetNhanVienByIdAsync(int id);
        Task AddNhanVienAsync(NhanVien nhanVien);
    
    }
}
