using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Controllers
{
    public class HomeController : AbpController
    {
        public readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
        [HttpGet("/test")]
        public IActionResult Get()
        {
            return Ok(_configuration.GetValue<string>("Test"));
        }
    }
}
