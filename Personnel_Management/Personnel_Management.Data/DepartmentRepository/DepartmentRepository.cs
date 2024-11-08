using Microsoft.EntityFrameworkCore;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly QuanLyNhanSuContext _context;

    public DepartmentRepository(QuanLyNhanSuContext context)
    {
        _context = context;
    }

    public async Task<List<PhongBan>> GetAllDepartmentsAsync()
    {
        return await _context.PhongBans.ToListAsync();
    }

    public async Task<PhongBan> GetDepartmentByIdAsync(int id)
    {
        return await _context.PhongBans.FindAsync(id);
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
}
