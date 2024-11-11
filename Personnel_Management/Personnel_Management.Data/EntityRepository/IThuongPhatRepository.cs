using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public interface IThuongPhatRepository
    {
        List<ThuongPhat> GetAllThuongPhat(int currentMonth, int currentYear, int nhanVienId);
        decimal GetTongThuongThang(int currentMonth, int currentYear, int nhanVienId);
        decimal GetTongPhatThang(int currentMonth, int currentYear, int nhanVienId);

        Luong GetLuongCoBan(int nhanVienId);

        ThuongPhat GetThuongPhatById(int thuongphatId);
        List<ThuongPhat> GetThuongPhatByNhanVienId(int nhanVienId);

        ThuongPhat AddThuongPhat(ThuongPhatAddModel thuongPhatAdd, int nhanVienId);
        bool UpdateThuongPhat(int thuongPhatId, ThuongPhatAddModel thuongPhatUpdate);

        bool DeleteThuongPhatById(int thuongPhatId);

    }
}
