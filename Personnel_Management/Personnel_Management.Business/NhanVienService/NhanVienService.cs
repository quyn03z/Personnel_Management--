using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using Personnel_Management.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.NhanVienService
{
	public class NhanVienService : INhanVienService
	{
		private readonly BaseRepository<NhanVien> _nhanVienRepository;

		public NhanVienService(BaseRepository<NhanVien> userRepository)
		{
			_nhanVienRepository = userRepository;
		}


		public async Task<NhanVienDTO> AddNhanVienAsync(NhanVien nhanVien)
		{
			nhanVien.Matkhau = _nhanVienRepository.HashPassword(nhanVien.Matkhau);

			await _nhanVienRepository.Add(nhanVien);

			return new NhanVienDTO
			{
				HoTen = nhanVien.HoTen,  
				NgaySinh = nhanVien.NgaySinh,
				DiaChi = nhanVien.DiaChi,
				SoDienThoai = nhanVien.SoDienThoai,
				Email = nhanVien.Email,
				Matkhau = nhanVien.Matkhau,
			};
		}

	}
}
