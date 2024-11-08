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
    Task AddDepartmentAsync(DepartmentDto departmentDto);
    Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
    Task DeleteDepartmentAsync(int id);
}
