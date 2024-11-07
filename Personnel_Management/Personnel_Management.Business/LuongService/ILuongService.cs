using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.LuongService
{
    public interface ILuongService
    {
        Task<Luong?> GetByIdAsync(int id);
        Task<Luong> AddAsync(Luong luong);
    }
}
