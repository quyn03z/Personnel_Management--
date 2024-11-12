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
        public async Task<Luong> GetByIdAsync(int id)
        {
            return await _luongRepository.GetLuongByIdAsync(id);
        }
        public async Task UpdateAsync(Luong luong)
        {
            var originalLuong = await _luongRepository.GetLuongByIdAsync(luong.NhanVienId);
            if (originalLuong == null) throw new Exception("Luong không tồn tại.");

            originalLuong.NhanVienId = luong.NhanVienId;
            originalLuong.LuongCoBan = luong.LuongCoBan;
            originalLuong.NgayCapNhat = luong.NgayCapNhat;
            originalLuong.PhuCap = luong.PhuCap;


            await _luongRepository.UpdateLuongAsync(originalLuong);
        }
    }
}
