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

    public async Task DeleteDepartmentAsync(int id)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(id);
        if (department == null)
            throw new KeyNotFoundException("Phòng ban không tồn tại.");

        await _departmentRepository.DeleteDepartmentAsync(department);
    }
}
