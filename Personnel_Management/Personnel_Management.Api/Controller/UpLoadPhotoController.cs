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


		[HttpGet("GetImagePath/{fileName}")]
		public IActionResult GetImagePath(string fileName)
		{
			// Xác định đường dẫn đầy đủ của tệp
			var filePath = Path.Combine(@"D:\Merger Personnel\Personnel_Management\Personnel_Management.Client\src\assets\img", fileName);

			// Kiểm tra xem tệp có tồn tại không
			if (!System.IO.File.Exists(filePath))
			{
				return NotFound(new { Message = "File not found" });
			}

			// Tạo đường dẫn tương đối để trả về cho Angular
			var relativePath = Path.Combine("assets/img", fileName).Replace("\\", "/"); // Thay thế \ thành / cho URL
			return Ok(new { ImagePath = relativePath });
		}





	}
}
