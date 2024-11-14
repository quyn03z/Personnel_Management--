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

			if (result.StartsWith("Extension is not valid") || result == "Maximum file size can be 5MB")
			{
				return BadRequest(new { Message = result });
			}
			return Ok(new { FileName = result, Message = "File uploaded successfully" });
		}



		[HttpGet("GetImage/{fileName}")]
		public IActionResult GetImage(string fileName)
		{
			var filePath = Path.Combine(@"D:\Merger Personnel\Personnel_Management\Personnel_Management.Client\src\assets\img", fileName);

			if (!System.IO.File.Exists(filePath))
			{
				return NotFound();
			}

			var image = System.IO.File.ReadAllBytes(filePath);
			return File(image, "image/jpeg"); // Thay đổi MIME type nếu cần
		}


	}
}
