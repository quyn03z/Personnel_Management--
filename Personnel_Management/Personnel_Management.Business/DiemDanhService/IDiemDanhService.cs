﻿using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.DiemDanhService
{
	public interface IDiemDanhService
	{
		Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id);
	}

}
