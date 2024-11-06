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
	public class NhanVienRepository : BaseRepository<NhanVien>, INhanVienRepository
	{
        private readonly QuanLyNhanSuContext _context;
		public NhanVienRepository(QuanLyNhanSuContext context) : base(context)
		{
            _context = context;
		}

        public IQueryable<NhanVien> GetQuery()
        {
            return _dbSet; // hoặc _context.NhanViens.AsQueryable()
        }

        public IQueryable<NhanVien> GetQuery(Expression<Func<NhanVien, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
        public async Task AddNhanVienAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NhanVien>> GetAllNhanViensAsync()
        {
            var nhanViens = await this.GetAllAsync();
            return nhanViens.ToList();
        }

        public async Task<NhanVien> GetNhanVienByIdAsync(int id)
        {
            return await _context.NhanViens.FindAsync(id);
        }

        public NhanVien Login(string email, string matkhau)
		{
			return _context.NhanViens
			.FirstOrDefault(user => user.Email == email && user.Matkhau == matkhau);
		}

        public List<NhanVien> GetAllManagerFunction()
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                list = _context.NhanViens.Where(nv => nv.RoleId == 3).Include(nv => nv.PhongBan).Include(nv => nv.DiemDanhs).Include(nv => nv.Luongs).Include(nv => nv.ThuongPhats).ToList();
            }
            catch (Exception e)
            {

                return null;
            }
            return list;
        }

        public NhanVien GetByIdManagerFunction(int id)
        {
            NhanVien nhanVien = null;
            try
            {
                nhanVien = _context.NhanViens
        .Where(nv => nv.NhanVienId == id)
        .Include(nv => nv.PhongBan)
        .Include(nv => nv.DiemDanhs)
        .Include(nv => nv.Luongs)
        .Include(nv => nv.ThuongPhats)
        .FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
            return nhanVien;
        }


    }

	}
