using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public class LuongRepository :BaseRepository<Luong>, ILuongRepository
    {
        private readonly QuanLyNhanSuContext _context;
        public LuongRepository(QuanLyNhanSuContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Luong> GetQuery()
        {
            return _dbSet; // hoặc _context.NhanViens.AsQueryable()
        }

        public IQueryable<Luong> GetQuery(Expression<Func<Luong, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
        public async Task AddLuongAsync(Luong luong)
        {
            _context.Luongs.Add(luong);
            await _context.SaveChangesAsync();
        }
        public async Task<Luong> GetLuongByIdAsync(int nhanVienId)
        {
            return await _context.Luongs.Where(l => l.NhanVienId == nhanVienId).FirstOrDefaultAsync(); ;
        }
        public async Task UpdateLuongAsync(Luong luong)
        {
            _context.Luongs.Update(luong);
            await _context.SaveChangesAsync();
        }

        public decimal GetLuongCoBanByNhanVienId(int nhanVienid)
        {

            decimal luongCoBan = 0;
            var luong = _context.Luongs.FirstOrDefault(l => l.NhanVien.NhanVienId == nhanVienid && l.NhanVien.isBanned == false);
            if (luong != null)
            {
                luongCoBan = luong.LuongCoBan;
            }
            return luongCoBan;
        }
    }
}
