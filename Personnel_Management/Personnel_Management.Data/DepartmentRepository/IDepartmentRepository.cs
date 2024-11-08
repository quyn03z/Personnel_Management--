using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDepartmentRepository
{
    Task<List<PhongBan>> GetAllDepartmentsAsync();
    Task<PhongBan> GetDepartmentByIdAsync(int id);
    Task AddDepartmentAsync(PhongBan department);
    Task UpdateDepartmentAsync(PhongBan department);
    Task DeleteDepartmentAsync(PhongBan department);
}
