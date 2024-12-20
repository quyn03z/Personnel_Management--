﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.DTO
{
	public class NhanVienDtto
	{
		[Required]

		public string HoTen { get; set; }

		public DateTime? NgaySinh { get; set; }

		public string DiaChi { get; set; }

		public string SoDienThoai { get; set; }

		[Required]
		public string Email { get; set; }

		public int RoleId { get; set; }

		public int? PhongBanId { get; set; }

		public string? PhongBanName { get; set; }
		public string? RoleName { get; set; }
		public string Avatar { get; set; }

		public string? Matkhau { get; set; }
	}
}
