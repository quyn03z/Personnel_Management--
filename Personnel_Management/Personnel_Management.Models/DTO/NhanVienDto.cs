using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.DTO
{
    public class NhanVienDto
    {
        public int NhanVienId { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int PhongBanId { get; set; }
        public bool isBanned { get; set; }
 
        public DepartmentDto PhongBan { get; set; }
    }
}
