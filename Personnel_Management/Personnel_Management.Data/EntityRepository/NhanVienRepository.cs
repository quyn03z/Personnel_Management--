using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.DTO;
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
        private readonly IMapper _mapper;
        private readonly QuanLyNhanSuContext _context;
		public NhanVienRepository(QuanLyNhanSuContext context, IMapper mapper) : base(context)
		{
            _mapper = mapper;
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
        public async Task UpdateNhanVienAsync(NhanVien nhanVien)
        {   
            _context.NhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();
        }
        public async Task<IQueryable<NhanVien>> GetAllNhanViensWithPhongBanAsync()
        {
            return _dbSet.Include(nv => nv.PhongBan);
        }
        public async Task<List<NhanVienDto>> GetAllNhanViensAsync()
        {
            var nhanViens = await (await this.GetAllNhanViensWithPhongBanAsync()).ToListAsync();

            if (nhanViens == null || !nhanViens.Any())
            {
                return new List<NhanVienDto>();
            }

            return _mapper.Map<List<NhanVienDto>>(nhanViens);
        }


        public async Task<NhanVien> GetNhanVienByIdAsync(int id)
        {
            return await _context.NhanViens.FindAsync(id);
        }

        
        public List<NhanVien> GetAllManagerFunction(int phongBanId)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                list = _context.NhanViens.Where(nv => nv.RoleId == 3 && nv.isBanned == false && nv.PhongBanId == phongBanId)
                    .Include(nv => nv.PhongBan)
                    .ToList();
            }
            catch (Exception e)
            {

                return null;
            }
            return list;
        }

        public NhanVien GetByIdManagerFunction(int id, int phongBanId)
        {
            NhanVien nhanVien = null;
            try
            {
                nhanVien = _context.NhanViens
        .Where(nv => nv.NhanVienId == id && nv.isBanned == false && nv.PhongBanId == phongBanId)
        .Include(nv => nv.PhongBan)
        .FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
            return nhanVien;
        }



		public NhanVien Login(string email, string matkhau)
		{
			return _context.NhanViens.SingleOrDefault(u => u.Email == email 
                                                        && u.Matkhau == HashPassword(matkhau)
                                                        && u.isBanned == false);
		}

		public async Task<NhanVien> GetById(int id)
		{
			return await _context.NhanViens.FindAsync(id);
		}

		public async Task<NhanVien> Update(NhanVien nhanVien)
		{
			_context.Update(nhanVien);
			await _context.SaveChangesAsync(); 
			return nhanVien;
		}





	}

}
