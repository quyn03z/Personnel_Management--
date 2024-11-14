using Personnel_Management.Models.Models;

public interface ILichNghiRepository
{
    List<DateTime> GetAllLichNghi2(int currentMonth, int currentYear);
    List<LichNghi> GetAllLichNghi();
    void AddLichNghi(LichNghi lichNghi);
    void DeleteLichNghiByExactDate(int day, int month, int year); // Delete by exact date
    void UpdateLichNghiByExactDate(int day, int month, int year, string newLyDo); // Update by exact date
}
