using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.Models
{
    public class LichNghi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LichNghiId { get; set; }

        [Required]
        public DateTime Ngay { get; set; }

        [Required]
        [MaxLength(255)]
        public string LyDo { get; set; } // Lý do nghỉ
    }
}
