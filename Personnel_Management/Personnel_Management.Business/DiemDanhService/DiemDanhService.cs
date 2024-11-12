using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.DiemDanhService
{
	public class DiemDanhService : IDiemDanhService
	{
		private readonly IDiemDanhRepository _diemDanhRepository;

		public DiemDanhService(IDiemDanhRepository diemDanhRepository)
		{
			_diemDanhRepository = diemDanhRepository;
		}

		public async Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id)
		{
			return await _diemDanhRepository.GetAllDiemDanhNhanVienByIdAsync(id);
		}

	}
}
