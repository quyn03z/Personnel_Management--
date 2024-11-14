using Personnel_Management.Models.Models;

public class LichNghiRepository : ILichNghiRepository
{
    private readonly QuanLyNhanSuContext _context;

    public LichNghiRepository(QuanLyNhanSuContext context)
    {
        _context = context;
    }

    public List<DateTime> GetAllLichNghi2(int currentMonth, int currentYear)
    {
        List<DateTime> list = _context.LichNghis.Where(ln => ln.Ngay.Month == currentMonth
                                            && ln.Ngay.Year == currentYear).Select(ln => ln.Ngay).ToList();
        return list;
    }
    // Lấy tất cả lịch nghỉ trong tháng với ngày và lý do
    public List<LichNghi> GetAllLichNghi()
    {
        return _context.LichNghis
            .ToList();  // Trả về danh sách đối tượng LichNghi đầy đủ
    }

    // Thêm lịch nghỉ
    public void AddLichNghi(LichNghi lichNghi)
    {
        _context.LichNghis.Add(lichNghi);
        _context.SaveChanges();
    }

    public void DeleteLichNghiByExactDate(int day, int month, int year)
    {
        var lichNghi = _context.LichNghis.FirstOrDefault(ln => ln.Ngay.Day == day && ln.Ngay.Month == month && ln.Ngay.Year == year);
        if (lichNghi != null)
        {
            _context.LichNghis.Remove(lichNghi);
            _context.SaveChanges();
        }
    }

    public void UpdateLichNghiByExactDate(LichNghi lichNghi)
    {

            _context.LichNghis.Update(lichNghi);
            _context.SaveChanges();

    }
    public LichNghi searchLichNghiByExactDate(int day, int month, int year)
    {

        return _context.LichNghis.FirstOrDefault(ln => ln.Ngay.Day == day && ln.Ngay.Month == month && ln.Ngay.Year == year);
    }
}