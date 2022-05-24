using Jiepei.ERP.Members.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members.Admin
{
    public abstract class MembersAdminAppService : ApplicationService
    {
        //private const string fileFilt = ".jpg|.jpeg|.png";
        //public IHttpContextAccessor HttpContext { get; set; }
        //public IHostingEnvironment HostingEnvironment { get; set; }
        protected MembersAdminAppService()
        {
            LocalizationResource = typeof(MembersResource);
            ObjectMapperContext = typeof(MembersAdminApplicationModule);
        }

        ///// <summary>
        ///// MD5加密
        ///// </summary>
        ///// <param name="password"></param>
        ///// 
        //protected string GetMD5Str(string password)
        //{
        //    var md5 = new MD5CryptoServiceProvider();
        //    var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    var newPwd = BitConverter.ToString(result).Replace("-", "");

        //    return newPwd;
        //}

        ///// <summary>
        ///// 获取当前登录人信息
        ///// </summary>
        ///// <returns></returns>
        //protected CurrentUserInfoDto GetCurrentUserInfo()
        //{
        //    if (HttpContext != null)
        //    {
        //        var Claims = HttpContext.HttpContext.User.Claims;
        //        if (Claims.Any())
        //        {
        //            var UserCode = Claims.FirstOrDefault(e => e.Type == "UserCode")?.Value;
        //            var userId = Claims.FirstOrDefault(e => e.Type == "Uid")?.Value;

        //            if (userId == null || UserCode == null)
        //                throw new UserFriendlyException("请先登录");
        //            userId = userId.ToUpper();
        //            var res = new CurrentUserInfoDto { UserId = new Guid(userId), UserCode = UserCode };

        //            return res;
        //        }
        //        else
        //        {
        //            throw new UserFriendlyException("请先登录");
        //        }

        //    }
        //    else
        //    {
        //        throw new UserFriendlyException("请先登录");
        //    }

        //}

        //protected async Task<string> UploadImgFile(IFormFile file)
        //{
        //    var currentDate = DateTime.Now;
        //    var webRootPath = HostingEnvironment.WebRootPath;//获取项目路径
        //    try
        //    {
        //        var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";
        //        //创建每日存储文件夹
        //        if (!Directory.Exists(webRootPath + filePath))
        //        {
        //            Directory.CreateDirectory(webRootPath + filePath);
        //        }
        //        if (file != null)
        //        {
        //            //文件后缀
        //            var fileExtension = Path.GetExtension(file.FileName);//获取文件格式.拓展名

        //            var fileSize = file.Length;

        //            //判断后缀是否是图片
        //            if (fileExtension == null)
        //                throw new UserFriendlyException("上传的文件没有后缀");
        //            if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
        //                throw new UserFriendlyException($"请上传{fileFilt}格式的文件");
        //            //判断文件大小
        //            if (fileSize > 1024 * 1024 * 2) //2M TODO:(1mb=1024X1024b)
        //            {
        //                throw new UserFriendlyException("请上传2M以内的文件");
        //            }
        //            //保存的文件名称(以会员code名称和保存时间命名)
        //            var name = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
        //            var saveName = $"{GetCurrentUserInfo().UserCode}_{name}_{ Guid.NewGuid().ToString()}{fileExtension}";

        //            //文件保存
        //            using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
        //            {
        //                await file.CopyToAsync(fs);
        //                fs.Flush();
        //            }
        //            //完整的文件路径
        //            var completeFilePath = Path.Combine(filePath, saveName);
        //            return completeFilePath;
        //        }
        //        else
        //        {
        //            throw new UserFriendlyException("请上传2M以内的文件");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UserFriendlyException($"文件上传失败:{ex.Message}");
        //    }
        //}

        ///// <summary>
        ///// 删除文件
        ///// </summary>
        ///// <param name="path"></param>
        //protected void DeleteFile(string path)
        //{
        //    var completeFilePath = HostingEnvironment.WebRootPath + path;
        //    if (File.Exists(completeFilePath))
        //    {
        //        File.Delete(HostingEnvironment.WebRootPath + path);
        //    }
        //}

        ///// <summary>
        ///// 生成验证码
        ///// </summary>
        ///// <param name="length">指定验证码的长度</param>
        ///// <returns></returns>
        //protected string CreateValidateCode(int length)
        //{
        //    int[] randMembers = new int[length];
        //    int[] validateNums = new int[length];
        //    string validateNumberStr = "";
        //    //生成起始序列值
        //    int seekSeek = unchecked((int)DateTime.Now.Ticks);
        //    Random seekRand = new Random(seekSeek);
        //    int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
        //    int[] seeks = new int[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        beginSeek += 10000;
        //        seeks[i] = beginSeek;
        //    }
        //    //生成随机数字
        //    for (int i = 0; i < length; i++)
        //    {
        //        Random rand = new(seeks[i]);
        //        int pownum = 1 * (int)Math.Pow(10, length);
        //        randMembers[i] = rand.Next(pownum, Int32.MaxValue);
        //    }
        //    //抽取随机数字
        //    for (int i = 0; i < length; i++)
        //    {
        //        string numStr = randMembers[i].ToString();
        //        int numLength = numStr.Length;
        //        Random rand = new Random();
        //        int numPosition = rand.Next(0, numLength - 1);
        //        validateNums[i] = int.Parse(numStr.Substring(numPosition, 1));
        //    }
        //    //生成验证码
        //    for (int i = 0; i < length; i++)
        //    {
        //        validateNumberStr += validateNums[i].ToString();
        //    }
        //    return validateNumberStr;
        //}
    }
}
