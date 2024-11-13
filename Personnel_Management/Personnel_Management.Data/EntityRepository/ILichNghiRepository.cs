using Personnel_Management.Models.Models;

public interface ILichNghiRepository
{
    List<DateTime> GetAllLichNghi2(int currentMonth, int currentYear);
    List<LichNghi> GetAllLichNghi(); // Trả về đối tượng LichNghi
    void AddLichNghi(LichNghi lichNghi); // Thêm một lịch nghỉ
    void DeleteLichNghi(int lichNghiId); // Xóa lịch nghỉ theo ID
}
