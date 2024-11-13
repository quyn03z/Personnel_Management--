﻿using Personnel_Management.Models.Models;

public class LichNghiRepository : ILichNghiRepository
{
    private readonly QuanLyNhanSuContext _context;

    public LichNghiRepository(QuanLyNhanSuContext context)
    {
        _context = context;
    }

    // Lấy tất cả lịch nghỉ trong tháng với ngày và lý do
    public List<LichNghi> GetAllLichNghi(int currentMonth, int currentYear)
    {
        return _context.LichNghis
            .Where(l => l.Ngay.Month == currentMonth && l.Ngay.Year == currentYear)
            .ToList();  // Trả về danh sách đối tượng LichNghi đầy đủ
    }

    // Thêm lịch nghỉ
    public void AddLichNghi(LichNghi lichNghi)
    {
        _context.LichNghis.Add(lichNghi);
        _context.SaveChanges();
    }

    // Xóa lịch nghỉ
    public void DeleteLichNghi(int lichNghiId)
    {
        var lichNghi = _context.LichNghis.Find(lichNghiId);
        if (lichNghi != null)
        {
            _context.LichNghis.Remove(lichNghi);
            _context.SaveChanges();
        }
    }
}