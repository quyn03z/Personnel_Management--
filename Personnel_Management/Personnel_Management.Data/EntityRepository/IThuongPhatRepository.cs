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
        List<ThuongPhat> GetAllThuongPhat(string fromDate, string toDate);
        List<ThuongPhat> GetThuongPhatByNhanVienId(int nhanVienId);

        ThuongPhat AddThuong(ThuongPhatAddModel thuong);
        ThuongPhat AddPhat(ThuongPhatAddModel phat);
        bool UpdateThuongPhat(int thuongPhatId, ThuongPhatAddModel thuongPhatUpdate);

        bool DeleteThuongPhatById(int thuongPhatId);

    }
}
