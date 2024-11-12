﻿using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public interface IDiemDanhRepository 
	{
		Task<List<DiemDanh>> GetAllDiemDanhNhanVienByIdAsync(int id);
	}

}
