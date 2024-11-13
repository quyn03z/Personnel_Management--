using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public interface ILichNghiRepository
    {
        List<DateTime> GetAllLichNghi(int currentMonth, int currentYear);
    }
}
