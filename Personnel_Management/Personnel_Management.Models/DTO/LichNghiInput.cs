using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.DTO
{
    public class LichNghiInput
    {
        [Required]
        public DateTime Ngay { get; set; }

        [Required]
        [MaxLength(255)]
        public string LyDo { get; set; } // Lý do nghỉ
    }

}
