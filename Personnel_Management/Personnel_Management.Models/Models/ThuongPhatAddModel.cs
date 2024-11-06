using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.Models
{
    public class ThuongPhatAddModel
    {
        public int ThuongPhatId { get; set; }

        [Required(ErrorMessage = "Nhân viên ID không được để trống")]
        public int NhanVienId { get; set; }

        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime Ngay { get; set; }

        [Required(ErrorMessage = "Số tiền không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải là nguyên dương và lớn hơn 0")]
        public decimal SoTien { get; set; }

        [Required(ErrorMessage = "Loại không được để trống")]
        public string Loai { get; set; }

        public string GhiChu { get; set; }
    }
}
