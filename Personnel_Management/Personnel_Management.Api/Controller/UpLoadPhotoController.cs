using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.UploadFileService;

namespace Personnel_Management.Api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class UpLoadPhotoController : ControllerBase
	{

		private readonly UploadService _uploadService;

		public UpLoadPhotoController(UploadService uploadService)
		{
			_uploadService = uploadService;
		}

		[HttpPost]
		public IActionResult UploadFile(IFormFile file)
		{
			var result = _uploadService.Upload(file);
			return Ok(result);
		}


	}
}
