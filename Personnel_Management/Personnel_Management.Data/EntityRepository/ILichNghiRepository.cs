using Personnel_Management.Models.Models;

public interface ILichNghiRepository
{
    List<LichNghi> GetAllLichNghi(int currentMonth, int currentYear); // Trả về đối tượng LichNghi
    void AddLichNghi(LichNghi lichNghi); // Thêm một lịch nghỉ
    void DeleteLichNghi(int lichNghiId); // Xóa lịch nghỉ theo ID
}
