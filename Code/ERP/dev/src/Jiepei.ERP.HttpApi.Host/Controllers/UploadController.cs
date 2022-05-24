using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : AbpController
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile formFile)
        {
            var fileName = DateTimeOffset.Now.ToUnixTimeSeconds() + "." + formFile.FileName.Split('.').Last();
            var rootPath = _env.ContentRootPath;
            var realtivePath = Path.Combine("upload", DateTime.Now.ToString("yyyyMM"));
            var absolutePath = Path.Combine(rootPath, realtivePath);
            var filePath = Path.Combine(absolutePath, fileName);
            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            if (formFile.Length > 0)
            {
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return Ok(new { filePath = Path.Combine(realtivePath, fileName) });
        }
    }
}
