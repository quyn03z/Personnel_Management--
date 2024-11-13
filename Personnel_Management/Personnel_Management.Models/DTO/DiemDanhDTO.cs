using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.DTO
{
	public class DiemDanhDTO
	{
		public int DiemDanhId { get; set; }

		public int NhanVienId { get; set; }

		public DateTime NgayDiemDanh { get; set; }

		public DateTime? ThoiGianVao { get; set; }

		public DateTime? ThoiGianRa { get; set; }

		public bool? TrangThai { get; set; }

		public string LyDoVangMat { get; set; }
	}
}
