using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

namespace Jiepei.ERP.Admin.Controllers
{
    [Route("api/aliyun-oss")]
    [ApiController]
    public class AliyunOssController : ERPController
    {
        private readonly AliyunOSSOptions _options;

        public AliyunOssController(IOptions<AliyunOSSOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        [HttpGet("policy-token")]
        public IActionResult GetPolicyToken()
        {
            var result = AliyunOSSHelper.GetPolicyToken(_options);
            return Ok(result);
        }

        [HttpPost("call-post")]
        public IActionResult CallBack()
        {
            return Ok(new { Status = "OK" });
        }

        [HttpPost("calculate-md5")]
        public string CalculateMD5(IFormFile file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = file.OpenReadStream())
                {
                    var hash = md5.ComputeHash(stream);
                    //var md5Str = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    //var plainTextBytes = Encoding.UTF8.GetBytes(md5Str);
                    return Convert.ToBase64String(hash);
                }
            }
        }
    }
}
