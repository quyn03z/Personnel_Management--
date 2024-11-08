using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.NhanVienService
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanVienService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        public async Task<IEnumerable<NhanVienDto>> GetAllAsync()
        {
            return await _nhanVienRepository.GetAllNhanViensAsync();
        }

        public async Task<NhanVien?> GetByIdAsync(int id)
        {
            return await _nhanVienRepository.GetNhanVienByIdAsync(id);
        }

        public async Task<NhanVien> AddAsync(NhanVien nhanVien)
        {
            var existingEmployeesInDepartment = await _nhanVienRepository.GetQuery(nv => nv.PhongBanId == nhanVien.PhongBanId).ToListAsync();

            if (existingEmployeesInDepartment.Count == 0)
            {
                // Phòng ban chưa có nhân viên, gán RoleId là 2 (Manager)
                nhanVien.RoleId = 2;
            }
            else
            {
                // Phòng ban đã có nhân viên, gán RoleId là 1 (Employee)
                nhanVien.RoleId = 1;
            }

            await _nhanVienRepository.AddNhanVienAsync(nhanVien);
            return nhanVien;
        }


        public async Task UpdateAsync(NhanVien nhanVien)
        {
            var originalNhanVien = await _nhanVienRepository.GetNhanVienByIdAsync(nhanVien.NhanVienId);
            if (originalNhanVien == null) throw new Exception("Nhân viên không tồn tại.");

            originalNhanVien.HoTen = nhanVien.HoTen;
            originalNhanVien.NgaySinh = nhanVien.NgaySinh;
            originalNhanVien.DiaChi = nhanVien.DiaChi;
            originalNhanVien.SoDienThoai = nhanVien.SoDienThoai;
            originalNhanVien.Email = nhanVien.Email;
            originalNhanVien.PhongBanId = nhanVien.PhongBanId;
            originalNhanVien.RoleId = nhanVien.RoleId;

            await _nhanVienRepository.UpdateNhanVienAsync(originalNhanVien);
        }


        //public async Task<NhanVien?> GetByEmailAsync(string email)
        //{
        //    return await _nhanVienRepository.GetByEmailAsync(email);
        //}

        public NhanVien Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(NhanVien user)
        {
            throw new NotImplementedException();
        }
    }
}
