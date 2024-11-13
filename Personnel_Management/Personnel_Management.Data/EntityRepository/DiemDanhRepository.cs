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
using static System.Net.Mime.MediaTypeNames;

namespace Personnel_Management.Data.EntityRepository
{
    public class DiemDanhRepository : BaseRepository<DiemDanh>, IDiemDanhRepository
    {
        private readonly QuanLyNhanSuContext _context;
        private readonly IMapper _mapper;
        public DiemDanhRepository(QuanLyNhanSuContext context, IMapper mapper): base(context)
        {
            _mapper = mapper;
			_context = context;
        }
        public List<DiemDanh> GetAllDiemDanhById(int currentMonth, int currentYear, int nhanVienId)
        {
            var danhSachDiemDanh = _context.DiemDanhs
                .Where(dd => dd.NhanVienId == nhanVienId &&
                 dd.NgayDiemDanh.Month == currentMonth &&
                 dd.NgayDiemDanh.Year == currentYear &&
                 dd.TrangThai == true)
                .ToList();

            return danhSachDiemDanh;
        }

        public int GetNumberOfDaysWorked(int currentMonth, int currentYear, int nhanVienId)
        {
            var soNgayDiLam = _context.DiemDanhs
                 .Count(dd => dd.NhanVienId == nhanVienId &&
                 dd.NgayDiemDanh.Month == currentMonth &&
                 dd.NgayDiemDanh.Year == currentYear &&
                 dd.TrangThai == true);

            return soNgayDiLam;
        }

        public int GetNgayCongChuan(int currentMonth, int currentYeat)
        {
            int soNgayCongChuan = 0;
            ILichNghiRepository lichNghiRepository = new LichNghiRepository(_context);

            //lay ra danh sach lich nghi
            var ngayLe = lichNghiRepository.GetAllLichNghi2(currentMonth, currentYeat);
            // Duyệt qua tất cả các ngày trong tháng
            DateTime firstDayOfMonth = new DateTime(currentYeat, currentMonth, 1);
            int daysInMonth = DateTime.DaysInMonth(currentYeat, currentMonth);
            for (int i = 0; i < daysInMonth; i++)
            {
                DateTime currentDay = firstDayOfMonth.AddDays(i);

                // Kiểm tra xem ngày hiện tại có phải là ngày làm việc (thứ 2 - thứ 6)
                if (currentDay.DayOfWeek != DayOfWeek.Saturday &&
                    currentDay.DayOfWeek != DayOfWeek.Sunday &&
                    !ngayLe.Contains(currentDay))  // Không phải ngày lễ
                {
                    soNgayCongChuan++;
                }
            }
            return soNgayCongChuan;
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


		public async Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id, int thang, int nam)
		{
			return await _context.DiemDanhs
						.Where(d => d.NhanVienId == id
						&& d.NgayDiemDanh.Month == thang
						&& d.NgayDiemDanh.Year == nam) 
						.ToListAsync();
		}
    }
}
