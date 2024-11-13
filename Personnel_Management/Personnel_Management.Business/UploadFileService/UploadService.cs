using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.UploadFileService
{
	public class UploadService
	{
		public string Upload(IFormFile file)
		{
			List<string> validExtensions = new List<string>() { ".jpg", ".png", ".gif" };
			string extension = Path.GetExtension(file.FileName).ToLower();
			if (!validExtensions.Contains(extension))
			{
				return $"Extension is not valid. Allowed extensions are: {string.Join(", ", validExtensions)}";
			}

			long size = file.Length;
			if (size > (5 * 1024 * 1024))
				return "Maximum file size can be 5MB";

			string fileName = Path.GetFileName(file.FileName);

			string folderPath = @"D:\Merger Personnel\Personnel_Management\Personnel_Management.Client\src\assets\img";
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
			string filePath = Path.Combine(folderPath, fileName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return fileName;
		}

	}
}
