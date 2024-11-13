using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DepartmentDto
{
    public string TenPhongBan { get; set; }
    public string MoTa { get; set; }
}
public class TotalNhanVienInPhongBanDto
{
    public int PhongBanId { get; set; }
    public string TenPhongBan { get; set; }
    public int TotalNhanVien { get; set; }
}
