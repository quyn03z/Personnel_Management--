using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.LuongService
{
    public class LuongService : ILuongService
    {
        private readonly ILuongRepository _luongRepository;
        public LuongService(ILuongRepository luongRepository)
        {
            _luongRepository = luongRepository;
        }
        public async Task<Luong> AddAsync(Luong luong)
        {
            await _luongRepository.AddLuongAsync(luong);
            return luong; 
        }
        public async Task<Luong?> GetByIdAsync(int id)
        {
            return await _luongRepository.GetLuongByIdAsync(id);
        }
    }
}
