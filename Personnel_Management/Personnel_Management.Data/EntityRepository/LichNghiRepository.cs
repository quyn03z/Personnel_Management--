using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
    public class LichNghiRepository : ILichNghiRepository
    {
        private readonly QuanLyNhanSuContext _context;

        public LichNghiRepository(QuanLyNhanSuContext context) {
            _context = context;
        }
        public List<DateTime> GetAllLichNghi(int currentMonth, int currentYear)
        {
            List<DateTime> list = _context.LichNghis.Where(ln => ln.Ngay.Month == currentMonth 
                                                && ln.Ngay.Year == currentYear).Select(ln => ln.Ngay).ToList();
            return list;
        }
    }
}
