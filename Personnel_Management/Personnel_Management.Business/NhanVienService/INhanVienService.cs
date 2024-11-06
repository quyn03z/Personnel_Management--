using Personnel_Management.Models.Models;
using Personnel_Management.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.NhanVienService
{
	public interface INhanVienService
	{
		Task<NhanVienDTO> AddNhanVienAsync(NhanVien nhanVien);

	}

}
