using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("call-back")]
        public IActionResult CallBack()
        {
            return Ok(new { Status = "OK" });
        }
    }
}
