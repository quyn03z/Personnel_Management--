﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Personnel_Management.Models.Models;

public partial class Luong
{
    public int LuongId { get; set; }

    public int NhanVienId { get; set; }

    public decimal LuongCoBan { get; set; }

    public decimal? PhuCap { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public virtual NhanVien NhanVien { get; set; }
}