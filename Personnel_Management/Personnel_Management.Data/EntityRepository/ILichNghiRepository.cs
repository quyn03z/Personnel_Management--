using Personnel_Management.Models.Models;

public interface ILichNghiRepository
{
    List<DateTime> GetAllLichNghi2(int currentMonth, int currentYear);
    List<LichNghi> GetAllLichNghi();
    void AddLichNghi(LichNghi lichNghi);
    void DeleteLichNghiByExactDate(int day, int month, int year); // Delete by exact date
    void UpdateLichNghiByExactDate(LichNghi lichnghi); // Update by exact date
    LichNghi searchLichNghiByExactDate(int day, int month, int year);
}
