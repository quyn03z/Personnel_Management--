using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public class DepartmentRepository : BaseRepository<PhongBan>, IDepartmentRepository
{
    private readonly QuanLyNhanSuContext _context;

    public DepartmentRepository(QuanLyNhanSuContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<PhongBan> GetQuery()
    {
        return _dbSet; // hoặc _context.NhanViens.AsQueryable()
    }

    public IQueryable<PhongBan> GetQuery(Expression<Func<PhongBan, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    public async Task<List<PhongBan>> GetAllDepartmentsAsync()
    {
        return await _context.PhongBans.ToListAsync();
    }

    public async Task<PhongBan> GetDepartmentByIdAsync(int id)
    {
        return await _context.PhongBans.FindAsync(id);
    }

    public async Task<List<NhanVien>> GetEmployeesByDepartmentIdAsync(int departmentId)
    {
        return await _context.NhanViens
                             .Where(nv => nv.PhongBanId == departmentId)
                             .ToListAsync();
    }

    public async Task AddDepartmentAsync(PhongBan department)
    {
        await _context.PhongBans.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(PhongBan department)
    {
        _context.PhongBans.Update(department);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDepartmentAsync(PhongBan department)
    {
        _context.PhongBans.Remove(department);
        await _context.SaveChangesAsync();
    }
    public async Task<List<TotalNhanVienInPhongBanDto>> GetTotalNhanVienInPhongBanAsync()
    {
        return await _context.PhongBans
            .Select(pb => new TotalNhanVienInPhongBanDto
            {
                PhongBanId = pb.PhongBanId,
                TenPhongBan = pb.TenPhongBan,
                TotalNhanVien = _context.NhanViens.Count(nv => nv.PhongBanId == pb.PhongBanId)
            }).OrderByDescending(d => d.TotalNhanVien)
            .ToListAsync();
    }
    public async Task<List<TotalNhanVienInPhongBanDto>> GetTopTotalNhanVienInPhongBanAsync(int count)
    {
        return await _context.PhongBans
            .Select(pb => new TotalNhanVienInPhongBanDto
            {
                PhongBanId = pb.PhongBanId,
                TenPhongBan = pb.TenPhongBan,
                TotalNhanVien = _context.NhanViens.Count(nv => nv.PhongBanId == pb.PhongBanId)
            }).OrderByDescending(d => d.TotalNhanVien)
            .Take(count)
            .ToListAsync();
    }
}
