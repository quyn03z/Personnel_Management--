using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Models.ModelsDTO;

namespace Personnel_Management.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NhanVienController : ControllerBase
	{
		private readonly INhanVienService _nhanVienService;

		public NhanVienController(INhanVienService nhanVienService)
		{
			_nhanVienService = nhanVienService;
		}


		// GET: api/Customer/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<NhanVienDTO>> GetCustomer(int id)
		{
			var nhanVien = await _nhanVienService.GetNhanVienById(id);
			if (nhanVien == null)
			{
				return NotFound(new { message = "NhanVien not found" });
			}
			return Ok(new { NhanVienDTO = nhanVien });
		}


	}
}
