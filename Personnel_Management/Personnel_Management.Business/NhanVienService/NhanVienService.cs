using Microsoft.EntityFrameworkCore;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.NhanVienService
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;
		private readonly QuanLyNhanSuContext _context;

		public NhanVienService(INhanVienRepository nhanVienRepository, QuanLyNhanSuContext context)
		{
			_nhanVienRepository = nhanVienRepository;
			_context = context;
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
                nhanVien.RoleId = 3 ;
            }
			nhanVien.isBanned = false;
			nhanVien.Matkhau = HashPassword(nhanVien.Matkhau);
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

		public async Task<NhanVienDtto> UpdateProfileEmployee(int id, NhanVienDtto nhanVienDTO)
		{
			var existingEmployee = await _nhanVienRepository.GetById(id);
			if (existingEmployee == null)
			{
				throw new ArgumentException($"Employee with ID {id} not found.");
			}

			existingEmployee.HoTen = nhanVienDTO.HoTen;
			existingEmployee.NgaySinh = nhanVienDTO.NgaySinh;
			existingEmployee.DiaChi = nhanVienDTO.DiaChi;
			existingEmployee.SoDienThoai = nhanVienDTO?.SoDienThoai;
			existingEmployee.Email = nhanVienDTO?.Email;
			existingEmployee.RoleId = nhanVienDTO.RoleId;
			existingEmployee.PhongBanId = nhanVienDTO.PhongBanId;
			existingEmployee.Avatar = nhanVienDTO?.Avatar;

			if (!string.IsNullOrEmpty(nhanVienDTO.Matkhau))
			{
				existingEmployee.Matkhau = nhanVienDTO.Matkhau;
			}

			await _nhanVienRepository.Update(existingEmployee);

			return new NhanVienDtto
			{
				HoTen = existingEmployee.HoTen,
				NgaySinh = existingEmployee.NgaySinh,
				DiaChi = existingEmployee.DiaChi,
				SoDienThoai = existingEmployee.SoDienThoai,
				Email = existingEmployee.Email,
				RoleId = existingEmployee.RoleId,
				PhongBanId = existingEmployee.PhongBanId,
				Avatar = existingEmployee.Avatar,
			};
		}


		public async Task<NhanVienDtto?> GetNhanVienById(int id)
		{
			var nhanVien = await _context.NhanViens
				.Include(nv => nv.PhongBan)
				.Include(nv => nv.Role)
				.FirstOrDefaultAsync(nv => nv.NhanVienId == id);

			if (nhanVien == null)
			{
				return null;
			}

			var nhanVienDto = new NhanVienDtto
			{
				HoTen = nhanVien.HoTen,
				NgaySinh = nhanVien.NgaySinh,
				DiaChi = nhanVien.DiaChi,
				SoDienThoai = nhanVien.SoDienThoai,
				Email = nhanVien.Email,
				PhongBanName = nhanVien.PhongBan?.TenPhongBan ?? "No Department", 
				RoleName = nhanVien.Role?.RoleName ?? "No Role",                  
				Avatar = nhanVien.Avatar,

			};

			return nhanVienDto;
		}
        public async Task<int> GetTotalEmployeesAsync()
        {
            return await _nhanVienRepository.GetQuery().CountAsync();
        }
        public async Task<bool> VerifyPasswordAsync(NhanVien nhanVien, string oldPassword)
		{
			// Giả sử mật khẩu đã lưu trong cơ sở dữ liệu là phiên bản đã mã hóa của mật khẩu
			string hashedOldPassword = HashPassword(oldPassword); // Mã hóa mật khẩu cũ để so sánh
			return nhanVien.Matkhau == hashedOldPassword;
		}

		public async Task<bool> ChangePasswordAsync(int id, string newPassword)
		{
			var nhanVien = await _nhanVienRepository.GetById(id);
			if (nhanVien == null)
			{
				return false; // Người dùng không tồn tại
			}

			// Mã hóa mật khẩu mới trước khi lưu
			nhanVien.Matkhau = HashPassword(newPassword);

			await _nhanVienRepository.Update(nhanVien);
			return true;
		}


		public string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(password);
				var hash = sha256.ComputeHash(bytes);
				return Convert.ToBase64String(hash);
			}
		}

	}
}
