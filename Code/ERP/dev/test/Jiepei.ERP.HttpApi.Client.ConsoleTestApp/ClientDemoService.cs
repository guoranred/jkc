using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.HttpApi.Client.ConsoleTestApp
{
    public class ClientDemoService : ITransientDependency
    {
        //private readonly IProfileAppService _profileAppService;

        //public ClientDemoService(IProfileAppService profileAppService)
        //{
        //    _profileAppService = profileAppService;
        //}

        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            await Task.Yield();
            //var output = await _profileAppService.GetAsync();
            //Console.WriteLine($"UserName : {output.UserName}");
            //Console.WriteLine($"Email    : {output.Email}");
            //Console.WriteLine($"Name     : {output.Name}");
            //Console.WriteLine($"Surname  : {output.Surname}");
        }
    }
}