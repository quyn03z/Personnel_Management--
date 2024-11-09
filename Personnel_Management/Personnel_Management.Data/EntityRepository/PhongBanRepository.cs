using Personnel_Management.Data.BaseRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.EntityRepository
{
	public class PhongBanRepository : BaseRepository<PhongBan>, IPhongBanRepository
	{
		public PhongBanRepository(QuanLyNhanSuContext context) : base(context)
		{
		}
	}
}
