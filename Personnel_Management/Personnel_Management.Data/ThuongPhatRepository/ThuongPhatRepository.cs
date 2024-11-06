using Microsoft.EntityFrameworkCore;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.ThuongPhatRepository
{
    public class ThuongPhatRepository : IThuongPhatRepository
    {
        private readonly QuanLyNhanSuContext _context;

        public ThuongPhatRepository(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        public ThuongPhat AddPhat(ThuongPhatAddModel phatAdd)
        {
            ThuongPhat phat;
            if (!phatAdd.Loai.Trim().ToLower().Equals("phat"))
            {
                return null;
            }
            try
            {
                phat = new ThuongPhat
                {
                    GhiChu = phatAdd.GhiChu,
                    Loai = phatAdd.Loai,
                    Ngay = phatAdd.Ngay,
                    NhanVienId = phatAdd.NhanVienId,
                    SoTien = phatAdd.SoTien
                };
                _context.ThuongPhats.Add(phat);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return phat;
        }

        public ThuongPhat AddThuong(ThuongPhatAddModel thuongAdd)
        {
            ThuongPhat thuong;
            if (!thuongAdd.Loai.Trim().ToLower().Equals("thuong"))
            {
                return null;
            }
            try
            {
                thuong = new ThuongPhat
                {
                    GhiChu = thuongAdd.GhiChu,
                    Loai = thuongAdd.Loai,
                    Ngay = thuongAdd.Ngay,
                    NhanVienId = thuongAdd.NhanVienId,
                    SoTien = thuongAdd.SoTien
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

        public List<ThuongPhat> GetAllThuongPhat(string fromDate, string toDate)
        {
            List<ThuongPhat> list = new List<ThuongPhat>();
            try
            {
                // Chuyển đổi từ chuỗi sang DateTime
                DateTime startDate = DateTime.Parse(fromDate);
                DateTime endDate = DateTime.Parse(toDate);

                // Lọc dữ liệu dựa trên khoảng ngày
                list = _context.ThuongPhats
                    .Where(tp => tp.Ngay >= startDate && tp.Ngay <= endDate).Include(tp => tp.NhanVien)
                    .ToList();
            }
            catch (Exception ex)
            {
                
                return null;
            }

            return list;
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

        public bool UpdateThuongPhat(int thuongPhatId, ThuongPhatAddModel thuongPhatUpdate)
        {
            bool check = false;
            //if (!thuongPhatUpdate.Loai.Trim().ToLower().Equals("thuong") || !thuongPhatUpdate.Loai.Trim().ToLower().Equals("phat"))
            //{
            //    return check;
            //}
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
