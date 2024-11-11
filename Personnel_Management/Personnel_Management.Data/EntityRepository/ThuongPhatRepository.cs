using Microsoft.EntityFrameworkCore;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public class ThuongPhatRepository : IThuongPhatRepository
    {
        private readonly QuanLyNhanSuContext _context;

        public ThuongPhatRepository(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        public ThuongPhat AddThuongPhat(ThuongPhatAddModel thuongPhatAdd, int nhanVienId)
        {
            ThuongPhat thuong;
            if (!thuongPhatAdd.Loai.Trim().ToLower().Equals("thuong") && !thuongPhatAdd.Loai.Trim().ToLower().Equals("phat"))
            {
                return null;
            }
            try
            {
                thuong = new ThuongPhat
                {
                    GhiChu = thuongPhatAdd.GhiChu,
                    Loai = thuongPhatAdd.Loai,
                    Ngay = thuongPhatAdd.Ngay,
                    NhanVienId = thuongPhatAdd.NhanVienId,
                    SoTien = thuongPhatAdd.SoTien
                };
                _context.ThuongPhats.Add(thuong);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return thuong;
        }

        public bool DeleteThuongPhatById(int thuongPhatId)
        {
            bool check = false;
            try
            {
                var thuongPhat = _context.ThuongPhats.FirstOrDefault(tp => tp.ThuongPhatId == thuongPhatId);
                if (thuongPhat == null)
                {
                    return check;
                }
                _context.ThuongPhats.Remove(thuongPhat);
                _context.SaveChanges();
                check = true;
            }
            catch (Exception e)
            {

                return check;
            }
            return check;
        }

        public List<ThuongPhat> GetAllThuongPhat(int currentMonth, int currentYear, int nhanVienId)
        {
            List<ThuongPhat> list = new List<ThuongPhat>();
            try
            {
                list = _context.ThuongPhats
            .Where(tp => tp.NhanVienId == nhanVienId && tp.Ngay.Month == currentMonth && tp.Ngay.Year == currentYear)
            .ToList();
                if (!list.Any())
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

            return list;
        }

        public Luong GetLuongCoBan(int nhanVienId)
        {
            var luong = _context.Luongs.FirstOrDefault(l => l.NhanVienId == nhanVienId);

            return luong;
        }

        public ThuongPhat GetThuongPhatById(int thuongphatId)
        {
            return _context.ThuongPhats.FirstOrDefault(tp => tp.ThuongPhatId == thuongphatId);
        }

        public List<ThuongPhat> GetThuongPhatByNhanVienId(int nhanVienId)
        {
            List<ThuongPhat> list = new List<ThuongPhat>();
            try
            {
                list = _context.ThuongPhats
                    .Where(tp => tp.NhanVienId == nhanVienId).Include(tp => tp.NhanVien)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Có thể log lỗi ở đây
                return null;
            }

            return list;
        }

        public decimal GetTongPhatThang(int currentMonth, int currentYear, int nhanVienId)
        {
            var tongPhat = _context.ThuongPhats
            .Where(tp => tp.NhanVienId == nhanVienId && tp.Ngay.Month == currentMonth && tp.Ngay.Year == currentYear && tp.Loai.Trim().ToLower() == "phat")
            .Sum(tp => tp.SoTien);

            return tongPhat;
        }

        public decimal GetTongThuongThang(int currentMonth, int currentYear, int nhanVienId)
        {
            var tongThuong = _context.ThuongPhats
            .Where(tp => tp.NhanVienId == nhanVienId && tp.Ngay.Month == currentMonth && tp.Ngay.Year == currentYear && tp.Loai.Trim().ToLower() == "thuong")
            .Sum(tp => tp.SoTien);

            return tongThuong;
        }

        public bool UpdateThuongPhat(int thuongPhatId, ThuongPhatAddModel thuongPhatUpdate)
        {
            bool check = false;
            if (!thuongPhatUpdate.Loai.Trim().ToLower().Equals("thuong") && !thuongPhatUpdate.Loai.Trim().ToLower().Equals("phat"))
            {
                return false;
            }
            try
            {
                var thuongPhat = _context.ThuongPhats.FirstOrDefault(tp => tp.ThuongPhatId == thuongPhatId);
                if (thuongPhat == null)
                {
                    return check;
                }

                thuongPhat.NhanVienId = thuongPhatUpdate.NhanVienId;
                thuongPhat.Ngay = thuongPhatUpdate.Ngay;
                thuongPhat.SoTien = thuongPhatUpdate.SoTien;
                thuongPhat.Loai = thuongPhatUpdate.Loai;
                thuongPhat.GhiChu = thuongPhatUpdate.GhiChu;

                _context.SaveChanges();
                check = true;
            }
            catch (Exception e)
            {

                return check;
            }
            return check;
        }
    }
}
