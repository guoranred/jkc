using Jiepei.ERP.Members.Consts;
using Jiepei.ERP.Members.CustomerServices.Dtos;
using Jiepei.ERP.Members.Enums;
using Jiepei.ERP.Members.Members.Dtos;
using Jiepei.ERP.Orders.Channels;
using Jiepei.Module.SMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Claims;
using JiePei.Abp.Sms.YiMei;
using Volo.Abp.Sms;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Members.CodeGenerations;
using Jiepei.ERP.CodeGenerations;

namespace Jiepei.ERP.Members
{
    /// <summary>
    /// 会员信息相关服务
    /// </summary>
    [Authorize]
    public class MemberAppService : MembersAppService, IMemberAppService
    {
        private const string fileFilt = ".jpg|.jpeg|.png";
        private readonly JwtConfig _jwtconfig;
        //private readonly ISmsSender _smsSender;
        private readonly IDistributedCache<string, string> _cache;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMemberInformationRepository _memberInformationRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly ISendSmsAppService _sendSmsAppService;
        private readonly IChannelAppService _channelAppService;
        private readonly ISmsSender _smsSender;
        private readonly ICodeGenerationRepository _codeGeneration;


        public MemberAppService(IOptions<JwtConfig> option
            , IDistributedCache<string, string> cache
            //, ISmsSender smsSender
            , IHostingEnvironment hostingEnvironment
            , IMemberInformationRepository memberInformationRepository
            , ICustomerServiceRepository customerServiceRepository
            , ISendSmsAppService sendSmsAppService, IChannelAppService channelAppService
            , ISmsSender smsSender
            , ICodeGenerationRepository codeGeneration)
        {
            _cache = cache;
            _jwtconfig = option.Value;
            //_smsSender = smsSender;
            _hostingEnvironment = hostingEnvironment;
            _memberInformationRepository = memberInformationRepository;
            _customerServiceRepository = customerServiceRepository;
            _sendSmsAppService = sendSmsAppService;
            _channelAppService = channelAppService;
            _smsSender = smsSender;
            _codeGeneration = codeGeneration;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task CreateAsync(RegisterInput input)
        {
            var other = await _memberInformationRepository.AnyAsync(e => e.PhoneNumber == input.PhoneNumber && e.ChannelId == input.ChannelId);
            if (other)
                throw new UserFriendlyException("当前手机号已注册");

            var CacheKey = $"{VerificationCodeTypeEnum.Register}_{input.PhoneNumber}";
            var ValidateCode = await _cache.GetAsync(CacheKey);

            if (string.IsNullOrWhiteSpace(ValidateCode))
                throw new UserFriendlyException("请先获取验证码");
            if (ValidateCode != input.ValidateCode)
                throw new UserFriendlyException("请输入正确的验证码");

            await _cache.RemoveAsync(CacheKey);
            //var BarCodeRule = await _BarCodeRuleManage.GetNextBarCodeRule(BarCodeRuleConsts.MemberCode);
            //var Code = BarCodeRule.GetCurrentBarCode();
            //随机分配客服
            var customerServices = (await _customerServiceRepository
                .GetListAsync(t => t.Type == CustomerServiceTypeEnum.CustomerService))
                .ToArray();
            var num = new Random().Next(0, customerServices.Length);

            var newMember = new MemberInformation(GuidGenerator.Create(),
                                                 input.ChannelId,
                                                 customerServices != null ? customerServices[num].Id : default,
                                                 input.PhoneNumber,
                                                 GetMD5Str(input.Password),
                                                 "",
                                                 input.Name,
                                                 input.Gender,
                                                 input.PromoCode,
                                                 input.Source.IsNullOrWhiteSpace() ? input.PromoCode : input.Source,
                                                 "PC");

            // 分配业务员
            if (!input.PromoCode.IsNullOrWhiteSpace())
            {
                var salesman = (await _customerServiceRepository.GetListAsync(e => e.PromoCode == input.PromoCode)).FirstOrDefault();
                if (salesman?.Type == CustomerServiceTypeEnum.SalesMan)
                {
                    newMember.SalesmanId = salesman.Id;
                }
                else
                {
                    await RandomAssignSalesman(newMember);
                }
            }
            else
            {
                await RandomAssignSalesman(newMember);
            }
            //生成唯一客户编号
            var n = await _codeGeneration.InsertAsync(new CodeGeneration(), true);
            var code = "K" + (100 + n.Id).ToString();
            newMember.SetCode(code);
            var member = await _memberInformationRepository.InsertAsync(newMember, true);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task CreateByAppAsync(RegisterAppInput input)
        {
            var other = await _memberInformationRepository.AnyAsync(e => e.PhoneNumber == input.PhoneNumber && e.ChannelId == input.ChannelId);
            if (other)
                throw new UserFriendlyException("当前手机号已注册");

            //var CacheKey = $"{VerificationCodeTypeEnum.Register}_{input.PhoneNumber}";
            //var ValidateCode = await _cache.GetAsync(CacheKey);

            //if (string.IsNullOrWhiteSpace(ValidateCode))
            //    throw new UserFriendlyException("请先获取验证码");
            //if (ValidateCode != input.ValidateCode)
            //    throw new UserFriendlyException("请输入正确的验证码");

            // await _cache.RemoveAsync(CacheKey);
            //var BarCodeRule = await _BarCodeRuleManage.GetNextBarCodeRule(BarCodeRuleConsts.MemberCode);
            //var Code = BarCodeRule.GetCurrentBarCode();
            //随机分配客服
            var customerServices = (await _customerServiceRepository
                .GetListAsync(t => t.Type == CustomerServiceTypeEnum.CustomerService))
                .ToArray();
            var num = new Random().Next(0, customerServices.Length);
            var newMember = new MemberInformation(GuidGenerator.Create(),
                                                  input.ChannelId,
                                                  customerServices != null ? customerServices[num].Id : default,
                                                  input.PhoneNumber,
                                                  GetMD5Str(input.PhoneNumber),//默认密码手机号
                                                  "",
                                                  input.Name,
                                                  GenderEnum.Male,
                                                  input.PromoCode,
                                                  input.Source.IsNullOrWhiteSpace() ? input.PromoCode : input.Source,
                                                  "App");

            // 分配业务员
            if (!input.PromoCode.IsNullOrWhiteSpace())
            {
                var salesman = (await _customerServiceRepository.GetListAsync(e => e.PromoCode == input.PromoCode)).FirstOrDefault();
                if (salesman?.Type == CustomerServiceTypeEnum.SalesMan)
                {
                    newMember.SalesmanId = salesman.Id;
                }
                else
                {
                    await RandomAssignSalesman(newMember);
                }
            }
            else
            {
                await RandomAssignSalesman(newMember);
            }
            //生成唯一客户编号
            var n = await _codeGeneration.InsertAsync(new CodeGeneration(), true);
            var code = "K" + (100 + n.Id).ToString();
            newMember.SetCode(code);
            var member = await _memberInformationRepository.InsertAsync(newMember, true);
        }

        public async Task<GetMemberDot> GetByIdAsync(Guid id)
        {
            var entity = await _memberInformationRepository.FindAsync(x => x.Id == id);
            return ObjectMapper.Map<MemberInformation, GetMemberDot>(entity); ;
        }

        public async Task<MemberBaseInfoOutputDto> GetAsync(Guid id)
        {
            var entity = await _memberInformationRepository.FindAsync(x => x.Id == id);
            return ObjectMapper.Map<MemberInformation, MemberBaseInfoOutputDto>(entity); ;
        }

        /// <summary>
        /// 获取tocken
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<LoginOutputDto> GetAsync(string phoneNumber, string password, Guid channelId)
        {
            var pwd = GetMD5Str(password);
            var memberInfo = await _memberInformationRepository
                .FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber && e.Password == pwd && e.ChannelId == channelId);
            if (memberInfo == null)
                throw new UserFriendlyException("用户名或密码错误");

            var tocken = CreateTocken(memberInfo);

            return new LoginOutputDto { Tocken = tocken, UserName = memberInfo.Name, Code = memberInfo.Code };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ChangePasswordInput input)
        {
            var Member = await _memberInformationRepository.FirstOrDefaultAsync(e => e.Id == CurrentUser.Id);
            if (Member == null)
                throw new UserFriendlyException("当前用户不存在或已被删除");

            var NewMd5Pwd = GetMD5Str(input.NewPassword);
            Member.Password = NewMd5Pwd;
            await _memberInformationRepository.UpdateAsync(Member);
            return true;
        }

        /// <summary>
        /// 修改会员基础信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMemBerAsync([FromForm] UpdateMemberInfoInput input)
        {
            var Member = await _memberInformationRepository.FirstOrDefaultAsync(e => e.Id == CurrentUser.Id && e.ChannelId == input.ChannelId);
            if (Member == null)
                throw new UserFriendlyException("当前用户不存在或已删被除");


            if (input.ProfilePhoto != null && input.ProfilePhoto.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(Member.ProfilePhotoUrl))
                    DeleteFile(Member.ProfilePhotoUrl);

                var path = await UploadImgFile(input.ProfilePhoto);
                Member.ProfilePhotoUrl = path;

            }

            Member.Name = input.Name;
            Member.QQ = input.QQ;
            Member.Gender = input.Gender;
            Member.ProvinceCode = input.ProvinceCode;
            Member.ProvinceName = input.ProvinceName;
            Member.CityCode = input.CityCode;
            Member.CityName = input.CityName;
            await _memberInformationRepository.UpdateAsync(Member);

            return true;
        }

        /// <summary>
        /// 获取当前登录人会员基础信息
        /// </summary>
        /// <returns></returns>
        public async Task<MemberBaseInfoOutputDto> GetMemBerAsync()
        {
            var Member = await _memberInformationRepository.FirstOrDefaultAsync(e => e.Id == CurrentUser.Id);
            if (Member == null)
                throw new UserFriendlyException("当前用户不存在或已被删除");

            return new MemberBaseInfoOutputDto
            {
                Code = Member.Code,
                Name = Member.Name,
                PhoneNumber = Member.PhoneNumber,
                QQ = Member.QQ,
                ProfilePhotoUrl = Member.ProfilePhotoUrl,
                CityCode = Member.CityCode,
                CityName = Member.CityName,
                Gender = Member.Gender,
                ProvinceCode = Member.ProvinceCode,
                ProvinceName = Member.ProvinceName
            };
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="Type">验证码类型</param>
        /// <param name="PhoneNumber">电话号码</param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> GetValidateCodeAsync(VerificationCodeTypeEnum Type, string PhoneNumber, Guid channelId)
        {
            var SMSTemple = "";
            var Member = await _memberInformationRepository.FirstOrDefaultAsync(e => e.PhoneNumber == PhoneNumber && e.ChannelId == channelId);
            var channel = await _channelAppService.GetAsync(channelId);

            switch (Type)
            {
                //注册
                case VerificationCodeTypeEnum.Register:
                    if (Member != null)
                        throw new UserFriendlyException("当前手机号已注册");
                    SMSTemple = "【" + channel.ChannelName + "智造】" + SMSConsts.Register;
                    break;
                //找回密码
                case VerificationCodeTypeEnum.FindPassword:
                    if (Member == null)
                        throw new UserFriendlyException("当前手机号还未注册");
                    SMSTemple = "【" + channel.ChannelName + "智造】" + SMSConsts.FindPassword;
                    break;
                default:
                    throw new UserFriendlyException("无效的请求");
            }
            //.随机生成验证码
            var ValidateCode = CreateValidateCode(6);
            //验证码加入缓存 有效期五分钟
            await _cache.SetAsync($"{Type}_{PhoneNumber}", ValidateCode, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(300)));//缓存5分钟

            //发送短信
            //  await _sendSmsAppService.Send(PhoneNumber, $"{SMSTemple}{ValidateCode}{SMSConsts.ValidityTime}{SMSConsts.BaseSMS}");
            await _smsSender.SendAsync(new SmsMessage(PhoneNumber, $"{SMSTemple}{ValidateCode}{SMSConsts.ValidityTime}{SMSConsts.BaseSMS}"));
            return true;
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> RetrievePasswordAsync(RetrievePasswordInput input)
        {

            var Member = await _memberInformationRepository.FirstOrDefaultAsync(e => e.PhoneNumber == input.PhoneNumber && e.ChannelId == input.ChannelId);
            if (Member == null)
                throw new UserFriendlyException("当前手机号还未注册");

            var CacheKey = $"{VerificationCodeTypeEnum.FindPassword}_{input.PhoneNumber}";
            var ValidateCode = await _cache.GetAsync(CacheKey);
            if (string.IsNullOrWhiteSpace(ValidateCode))
                throw new UserFriendlyException("请先获取验证码");

            if (ValidateCode != input.ValidateCode)
                throw new UserFriendlyException("请输入正确的验证码");

            var NewPwd = GetMD5Str(input.Password);

            Member.Password = NewPwd;

            await _memberInformationRepository.UpdateAsync(Member);

            await _cache.RemoveAsync(CacheKey);
            return true;
        }

        /// <summary>
        /// 获取当前用户客服信息
        /// </summary>
        /// <returns></returns>
        public async Task<GetCurrentUserCustomerServiceOutputDto> GetCurrentUserCustomerService()
        {
            var res = new GetCurrentUserCustomerServiceOutputDto();
            var UserId = CurrentUser.Id;
            var MemberInfo = await _memberInformationRepository.GetAsync(e => e.Id == UserId);
            if (MemberInfo == null)
                throw new UserFriendlyException("当前会员信息异常,请重新登录");
            if (MemberInfo.CustomerServiceId != null)
            {
                var customer = await _customerServiceRepository.GetAsync(e => e.Id == MemberInfo.CustomerServiceId);
                res = ObjectMapper.Map<CustomerService, GetCurrentUserCustomerServiceOutputDto>(customer);
            }

            return res;
        }



        /// <summary>
        /// 生成tocken
        /// </summary>
        /// <param name="loginUserInfo"></param>
        /// <returns></returns>
        private string CreateTocken(MemberInformation loginUserInfo)
        {
            DateTime UTC = DateTime.UtcNow;
            Claim[] claims = new Claim[]
            {
                new Claim("sub",loginUserInfo.Id.ToString()),
                new Claim("role","admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//JWT ID,JWT的唯一标识
                new Claim(JwtRegisteredClaimNames.Iat, UTC.ToString(), ClaimValueTypes.Integer64),//Issued At，JWT颁发的时间，采用标准unix时间，用于验证过期
                new Claim(AbpClaimTypes.Name,loginUserInfo.Name),
                new Claim("userId",loginUserInfo.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtconfig.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new(

            issuer: _jwtconfig.Issuer,//jwt签发者,非必须
            audience: _jwtconfig.Audience,//jwt的接收该方，非必须
            claims: claims,//声明集合
            expires: UTC.AddHours(12),//指定token的生命周期，unix时间戳格式,非必须
            signingCredentials: creds);//使用私钥进行签名加密

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);//生成最后的JWT字符串


            return encodedJwt;
        }

        private async Task<string> UploadImgFile(IFormFile file)
        {
            var currentDate = DateTime.Now;
            var webRootPath = _hostingEnvironment.WebRootPath;//获取项目路径
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            try
            {
                var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";
                //创建每日存储文件夹
                if (!Directory.Exists(webRootPath + filePath))
                {
                    Directory.CreateDirectory(webRootPath + filePath);
                }
                if (file != null)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(file.FileName);//获取文件格式.拓展名

                    var fileSize = file.Length;

                    //判断后缀是否是图片
                    if (fileExtension == null)
                        throw new UserFriendlyException("上传的文件没有后缀");
                    if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                        throw new UserFriendlyException($"请上传{fileFilt}格式的文件");
                    //判断文件大小
                    if (fileSize > 1024 * 1024 * 2) //2M TODO:(1mb=1024X1024b)
                    {
                        throw new UserFriendlyException("请上传2M以内的文件");
                    }
                    //保存的文件名称(以会员 Id 和保存时间命名)
                    var name = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
                    var saveName = $"{CurrentUser.Id}_{name}{fileExtension}";

                    //文件保存
                    using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);
                    return completeFilePath;
                }
                else
                {
                    throw new UserFriendlyException("请上传2M以内的文件");
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"文件上传失败:{ex.Message}");
            }
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        private void DeleteFile(string path)
        {
            var completeFilePath = _hostingEnvironment.WebRootPath + path;
            if (File.Exists(completeFilePath))
            {
                File.Delete(_hostingEnvironment.WebRootPath + path);
            }
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        private string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = int.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        ///
        protected string GetMD5Str(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var newPwd = BitConverter.ToString(result).Replace("-", "");

            return newPwd;
        }

        private async Task RandomAssignSalesman(MemberInformation member)
        {
            var salesmen = await _customerServiceRepository
                .GetListAsync(t => t.Type == CustomerServiceTypeEnum.SalesMan && t.IsOnline);
            var index = new Random().Next(0, salesmen.Count - 1);
            member.SalesmanId = salesmen?[index].Id;
        }

        /// <summary>
        /// 根据主键ID获取客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerServiceDto> GetCustomerServiceAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerService, CustomerServiceDto>(
                await _customerServiceRepository.GetAsync(id));
        }
        public async Task<CodeGenerationDto> GetCodeGeneration()
        {
            var entiy = await _codeGeneration.InsertAsync(new CodeGeneration(),true);
            return ObjectMapper.Map<CodeGeneration, CodeGenerationDto>(entiy);
        }
    }
}
