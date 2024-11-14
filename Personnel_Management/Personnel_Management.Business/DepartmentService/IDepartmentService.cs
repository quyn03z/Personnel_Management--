using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDepartmentService
{
    Task<List<PhongBan>> GetAllDepartmentsAsync();
    Task<PhongBan> GetDepartmentByIdAsync(int id);
    Task<List<NhanVien>> GetEmployeesByDepartmentIdAsync(int departmentId);
    Task AddDepartmentAsync(DepartmentDto departmentDto);
    Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
    Task DeleteDepartmentAsync(int id);
    Task<bool> HasEmployeesAsync(int departmentId);
    Task<int> GetTotalDepartmentAsync();
    Task<List<TotalNhanVienInPhongBanDto>> GetTotalNhanVienInPhongBanAsync();
    Task<List<TotalNhanVienInPhongBanDto>> GetTopTotalNhanVienInPhongBanAsync(int count);
}

