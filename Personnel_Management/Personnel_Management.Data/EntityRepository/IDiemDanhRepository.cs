using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public interface IDiemDanhRepository 
	{
		Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id, int thang, int nam);

		Task<DiemDanh> AddDiemDanhNhanVienCo(DiemDanhDTO diemDanh);
		Task<DiemDanh> AddDiemDanhNhanVienVang(DiemDanhDTO diemDanh);


	}

}
