using Microsoft.EntityFrameworkCore;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Personnel_Management.Data.EntityRepository
{
    public class DiemDanhRepository : IDiemDanhRepository
    {
        private readonly QuanLyNhanSuContext _context;
        public DiemDanhRepository(QuanLyNhanSuContext context)
        {
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
            var ngayLe = lichNghiRepository.GetAllLichNghi(currentMonth, currentYeat);
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
    }
}
