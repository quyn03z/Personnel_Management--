using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.DiemDanhService;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiemDanhController : ControllerBase
	{
		private readonly IDiemDanhService _diemDanhService;

		public DiemDanhController(IDiemDanhService diemDanhService)
		{
			_diemDanhService = diemDanhService;
		}


		[HttpGet("GetDiemDanhByNhanVienId/{id}")]
		public async Task<ActionResult<IEnumerable<DiemDanh>>> GetDiemDanhByNhanVienId(int id)
		{
			var diemDanhList = await _diemDanhService.GetAllDiemDanhNhanVienByIdAsync(id);

			if (diemDanhList == null || !diemDanhList.Any())
			{
				return NotFound();
			}

			return Ok(diemDanhList);
		}

	}

}
