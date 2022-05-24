using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Jiepei.ERP.Utilities
{
    /// <summary>
    /// 阿里云上传
    /// https://help.aliyun.com/document_detail/171344.html
    /// </summary>

    internal class PolicyConfig
    {
        public string Expiration { get; set; }
        public List<List<Object>> Conditions { get; set; }
    }

    public class PolicyToken
    {
        public string AccessId { get; set; }
        public string Policy { get; set; }
        public string Signature { get; set; }
        public string Dir { get; set; }
        public string Host { get; set; }
        public string Expire { get; set; }
        public string Callback { get; set; }
    }
    internal class CallbackParam
    {
        public string CallbackUrl { get; set; }
        public string CallbackBody { get; set; }
        public string CallbackBodyType { get; set; }
    }


    public static class AliyunOSSHelper
    {
        // 用户上传文件时指定的前缀。
        public static string uploadDir = $"bianselong/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTimeOffset.Now.ToUnixTimeMilliseconds()}/";
        public static int expireTime = 30;
        public static PolicyToken GetPolicyToken(AliyunOSSOptions options)
        {
            var expireDateTime = DateTime.Now.AddSeconds(expireTime);
            //policy
            var config = new PolicyConfig();
            config.Expiration = FormatIso8601Date(expireDateTime);
            config.Conditions = new List<List<Object>>();

            config.Conditions.Add(new List<Object>());
            config.Conditions[0].Add("content-length-range");
            config.Conditions[0].Add(0);
            config.Conditions[0].Add(1048576000);

            config.Conditions.Add(new List<Object>());
            config.Conditions[1].Add("starts-with");
            config.Conditions[1].Add("$key");
            config.Conditions[1].Add(uploadDir);

            var policy = JsonConvert.SerializeObject(config,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            var policy_base64 = EncodeBase64("utf-8", policy);
            var signature = ComputeSignature(options.AccessKeySecret, policy_base64);

            //callback
            var callback = new CallbackParam();
            callback.CallbackUrl = options.CallbackUrl;
            callback.CallbackBody = "filename=${object}&size=${size}&mimeType=${mimeType}&height=${imageInfo.height}&width=${imageInfo.width}";
            callback.CallbackBodyType = "application/x-www-form-urlencoded";

            var callback_string = JsonConvert.SerializeObject(callback);
            var callback_string_base64 = EncodeBase64("utf-8", callback_string);

            var policyToken = new PolicyToken();

            policyToken.AccessId = options.AccessKeyId;
            policyToken.Host = options.Host;
            policyToken.Policy = policy_base64;
            policyToken.Signature = signature;
            policyToken.Expire = ToUnixTime(expireDateTime);
            policyToken.Callback = callback_string_base64;
            policyToken.Dir = uploadDir;

            return policyToken;
        }
        private static string FormatIso8601Date(DateTime dtime) => dtime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.CurrentCulture);

        private static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        private static string ComputeSignature(string key, string data)
        {
            using (var algorithm = new HMACSHA1())
            {
                algorithm.Key = Encoding.UTF8.GetBytes(key.ToCharArray());
                return Convert.ToBase64String(
                    algorithm.ComputeHash(Encoding.UTF8.GetBytes(data.ToCharArray())));
            }
        }

        private static string ToUnixTime(DateTime dtime)
        {
            const long ticksOf1970 = 621355968000000000;
            var expires = ((dtime.ToUniversalTime().Ticks - ticksOf1970) / 10000000L)
                .ToString(CultureInfo.InvariantCulture);

            return expires;
        }
    }
}
