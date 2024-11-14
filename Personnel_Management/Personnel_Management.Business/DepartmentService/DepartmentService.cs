using Microsoft.EntityFrameworkCore;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<PhongBan>> GetAllDepartmentsAsync()
    {
        return await _departmentRepository.GetAllDepartmentsAsync();
    }

    public async Task<PhongBan> GetDepartmentByIdAsync(int id)
    {
        return await _departmentRepository.GetDepartmentByIdAsync(id);
    }

    public async Task<List<NhanVien>> GetEmployeesByDepartmentIdAsync(int departmentId)
    {
        return await _departmentRepository.GetEmployeesByDepartmentIdAsync(departmentId);
    }

    public async Task AddDepartmentAsync(DepartmentDto departmentDto)
    {
        var department = new PhongBan
        {
            TenPhongBan = departmentDto.TenPhongBan,
            MoTa = departmentDto.MoTa
        };
        await _departmentRepository.AddDepartmentAsync(department);
    }

    public async Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
    {
        var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
        if (existingDepartment == null)
            throw new KeyNotFoundException("Phòng ban không tồn tại.");

        existingDepartment.TenPhongBan = departmentDto.TenPhongBan;
        existingDepartment.MoTa = departmentDto.MoTa;

        await _departmentRepository.UpdateDepartmentAsync(existingDepartment);
    }

    public async Task<bool> HasEmployeesAsync(int departmentId)
    {
        var employees = await _departmentRepository.GetEmployeesByDepartmentIdAsync(departmentId);
        return employees != null && employees.Any();
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(id);
        if (department == null)
            throw new KeyNotFoundException();

        // Kiểm tra nếu phòng ban có nhân viên
        if (await HasEmployeesAsync(id))
            throw new InvalidOperationException();

        await _departmentRepository.DeleteDepartmentAsync(department);
    }

    public async Task<List<TotalNhanVienInPhongBanDto>> GetTopTotalNhanVienInPhongBanAsync(int count)
    {
        return await _departmentRepository.GetTopTotalNhanVienInPhongBanAsync(count);
    }
    public async Task<List<TotalNhanVienInPhongBanDto>> GetTotalNhanVienInPhongBanAsync()
    {
        return await _departmentRepository.GetTotalNhanVienInPhongBanAsync();
    }
    public async Task<int> GetTotalDepartmentAsync()
    {
        return await _departmentRepository.GetQuery().CountAsync();
    }
}
