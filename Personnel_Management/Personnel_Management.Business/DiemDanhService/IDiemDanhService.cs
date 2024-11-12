using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.DiemDanhService
{
	public interface IDiemDanhService
	{
		Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id);

		Task<DiemDanh> AddDiemDanhCoAsync(DiemDanhDTO diemDanh);
		Task<DiemDanh> AddDiemDanhVangAsync(DiemDanhDTO diemDanh);

	}

}
