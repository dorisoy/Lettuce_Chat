using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lettuce_Chat.Controllers
{
    public class FileTransferController : Controller
    {
        private IWebHostEnvironment Env { get; set; }
        public FileTransferController(IWebHostEnvironment HostEnv)
        {
            Env = HostEnv;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Download()
        {
            var di = Directory.CreateDirectory(Path.Combine(Env.ContentRootPath, "Data\\File_Transfers"));
            foreach (var file in di.GetFiles())
            {
                if (DateTime.Now - file.CreationTime > TimeSpan.FromDays(7))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch { }
                }
            }
            if (!Request.Query.ContainsKey("file"))
            {
                return new BadRequestResult();
            }
            var fileName = Request.Query["file"];
            Response.Headers.Add("Content-Disposition", new Microsoft.Extensions.Primitives.StringValues($"attachment; filename=\"{fileName}\""));
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            var filePath = Path.Combine(Env.ContentRootPath, "Data\\File_Transfers", fileName);
            if (System.IO.File.Exists(filePath))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(filePath), Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream"));
            }
            else
            {
                return new NoContentResult();
            }
        }

        [HttpPost]
        public IActionResult Upload(ICollection<IFormFile> Files)
        {
            var di = Directory.CreateDirectory(Path.Combine(Env.ContentRootPath, "Data\\File_Transfers"));
            var fileList = Request.Form.Files.ToList();
            foreach (var file in fileList)
            {
                var filName = Path.GetFileNameWithoutExtension(file.FileName);
                var extName = Path.GetExtension(file.FileName);
                var count = 0;
                while (System.IO.File.Exists(Path.Combine(di.FullName, filName + extName)))
                {
                    filName = Path.GetFileNameWithoutExtension(file.FileName) + count.ToString();
                    count++;
                }
                using (var fs = new FileStream(Path.Combine(di.FullName, filName + extName), FileMode.Create))
                {
                    file.OpenReadStream().CopyTo(fs);
                }
                return new OkObjectResult(filName + extName);
            }
            return new OkResult();
        }
    }
}
