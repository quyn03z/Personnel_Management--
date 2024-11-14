using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public interface IDepartmentRepository
{
    IQueryable<PhongBan> GetQuery();
    IQueryable<PhongBan> GetQuery(Expression<Func<PhongBan, bool>> predicate);
    Task<List<PhongBan>> GetAllDepartmentsAsync();
    Task<PhongBan> GetDepartmentByIdAsync(int id);
    Task<List<NhanVien>> GetEmployeesByDepartmentIdAsync(int departmentId);
    Task AddDepartmentAsync(PhongBan department);
    Task UpdateDepartmentAsync(PhongBan department);
    Task DeleteDepartmentAsync(PhongBan department);
    Task<List<TotalNhanVienInPhongBanDto>> GetTotalNhanVienInPhongBanAsync();
    Task<List<TotalNhanVienInPhongBanDto>> GetTopTotalNhanVienInPhongBanAsync(int count);

}
