using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechMed.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class HandleFileUploadController : Controller
	{
		[HttpPost]
		public async Task<IActionResult> UploadImage(IFormFile upload)
		{
			if (upload != null && upload.Length > 0)
			{
				var uniqueFileName = Guid.NewGuid().ToString() + "_" + upload.FileName;
				// Lưu ảnh vào thư mục của ứng dụng
				var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/CKFile", uniqueFileName);
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await upload.CopyToAsync(stream);
				}

				return new JsonResult(new { path = "/Upload/CKFile" + uniqueFileName });
			}
			return RedirectToAction("create");
		}

		[HttpGet]
		public IActionResult UploadExplore()
		{
			var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/CKFile"));
			ViewBag.fileInfor = dir.GetFiles();
			return View();
		}

        [HttpPost]
        public IActionResult DeleteFile(string fileUrl)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/CKFile", fileUrl);
            // Kiểm tra xem tệp ảnh tồn tại không
            if (System.IO.File.Exists(imagePath))
            {
                // Nếu tệp ảnh tồn tại, xóa nó khỏi thư mục
                System.IO.File.Delete(imagePath);
            }
            return new JsonResult("Xóa thành công");
        }
    }
}
