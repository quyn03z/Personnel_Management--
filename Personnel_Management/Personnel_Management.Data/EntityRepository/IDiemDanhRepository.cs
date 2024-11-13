using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public interface IDiemDanhRepository
    {
        List<DiemDanh> GetAllDiemDanhById(int currentMonth, int currentYear, int nhanVienId);
        int GetNumberOfDaysWorked(int currentMonth, int currentYear, int nhanVienId);
        int GetNgayCongChuan(int currentMonth, int currentYeat);
    }
}
