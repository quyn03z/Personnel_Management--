using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public interface ILuongRepository
    {
        IQueryable<Luong> GetQuery();
        IQueryable<Luong> GetQuery(Expression<Func<Luong, bool>> predicate);
        Task AddLuongAsync(Luong luong);
        Task<Luong> GetLuongByIdAsync(int id);
        decimal GetLuongCoBanByNhanVienId(int nhanVienid);
        Task UpdateLuongAsync(Luong luong);
    }
}
