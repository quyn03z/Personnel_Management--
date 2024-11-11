using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Models.ModelsDTO
{
	public class LoginDTO
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string MatKhau { get; set; }
	}

}
