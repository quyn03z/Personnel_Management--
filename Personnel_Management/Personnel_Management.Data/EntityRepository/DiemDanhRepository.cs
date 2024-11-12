using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public class DiemDanhRepository : BaseRepository<DiemDanh>, IDiemDanhRepository
	{
		private readonly IMapper _mapper;
		private readonly QuanLyNhanSuContext _context;
		public DiemDanhRepository(QuanLyNhanSuContext context, IMapper mapper) : base(context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<DiemDanh> AddDiemDanhNhanVienCo(DiemDanhDTO diemDanhDTO)
		{
			var diemDanh = new DiemDanh
			{
				NhanVienId = diemDanhDTO.NhanVienId,
				NgayDiemDanh = diemDanhDTO.NgayDiemDanh,
				TrangThai = diemDanhDTO.TrangThai,
				ThoiGianVao = diemDanhDTO.ThoiGianVao,
				ThoiGianRa = diemDanhDTO.ThoiGianRa,
			};

			_context.DiemDanhs.Add(diemDanh);
			await _context.SaveChangesAsync();
			return diemDanh;
		}

		public async Task<DiemDanh> AddDiemDanhNhanVienVang(DiemDanhDTO diemDanhDTO)
		{
			var diemDanh = new DiemDanh
			{
				NhanVienId = diemDanhDTO.NhanVienId,
				NgayDiemDanh = diemDanhDTO.NgayDiemDanh,
				TrangThai = diemDanhDTO.TrangThai,
				LyDoVangMat = diemDanhDTO.LyDoVangMat,
			};

			_context.DiemDanhs.Add(diemDanh);
			await _context.SaveChangesAsync();
			return diemDanh;
		}


		public async Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id)
		{
			return await _context.DiemDanhs
						.Where(d => d.NhanVienId == id) 
						.ToListAsync();
		}

		
	}

}
