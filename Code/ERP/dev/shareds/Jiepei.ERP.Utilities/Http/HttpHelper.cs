using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Jiepei.ERP.Utilities
{
    public class HttpHelper
    {

        static readonly List<string> _userAgentList = new List<string>(10);

        /// <summary>
        /// 站点编码,GB2312,UTF-8等
        /// </summary>
        public Encoding Encode { get; set; }

        /// <summary>
        /// Cookie
        /// </summary>
        public CookieContainer CookieContainer { get; private set; }

        /// <summary>
        /// 是否压缩，未实现
        /// </summary>
        public bool? IsGZip { get; set; }

        /// <summary>
        /// 是否允许跳转。Get或Post之前设置。
        /// </summary>
        public bool? AllowAutoRedirect { get; set; }

        /// <summary>
        /// 用户代码头
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// KeepAlive  始终false
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 请求url列表
        /// </summary>
        public List<string> RequestUrlList { get; private set; }

        static HttpHelper()
        {
            _userAgentList.Add("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
            _userAgentList.Add("User-Agent	Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)");
            _userAgentList.Add("Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0");
        }

        public HttpHelper(string encoding)
            : this(Encoding.GetEncoding(encoding))
        {

        }
        public HttpHelper(Encoding encoding)
            : this(encoding, null, null)
        {

        }
        public HttpHelper(Encoding e, bool? isGzip, bool? isAllowAutoRedirect)
        {
            this.Encode = e;
            this.IsGZip = isGzip;
            this.AllowAutoRedirect = isAllowAutoRedirect;

            this.RequestUrlList = new List<string>(20);
            this.UserAgent = _userAgentList[Math.Abs(Guid.NewGuid().GetHashCode() % _userAgentList.Count)];
            this.CookieContainer = new CookieContainer();
        }

        public static void Add(string userAgent)
        {
            _userAgentList.Add(userAgent);
        }


        public class HttpResult
        {
            /// <summary>
            /// 返回状态码
            /// </summary>
            public int StatusCode { get; set; }

            /// <summary>
            /// 返回的html代码
            /// </summary>
            public string ResultHtml { get; set; }

            public byte[] Data { get; set; }

            /// <summary>
            /// 响应头
            /// </summary>
            public WebHeaderCollection ResponseHeader { get; set; }

        }

        /// <summary>
        /// Get请头，默认前导页面为上一次请求的地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public HttpResult Get(string url)
        {
            return Get(url, null, new NameValueCollection());
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static T Get<T>(string url, string contentType)
        {
            try
            {
                string retString = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = contentType;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(myResponseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                myResponseStream.Close();
                return JsonConvert.DeserializeObject<T>(retString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Get<T>(string url, NameValueCollection requestHeaders) where T : class
        {
            string retString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            if (requestHeaders != null)
            {
                foreach (string item in requestHeaders.Keys)
                {
                    request.Headers[item] = requestHeaders[item];
                }
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(myResponseStream);
            retString = streamReader.ReadToEnd();
            streamReader.Close();
            myResponseStream.Close();
            return JsonConvert.DeserializeObject<T>(retString);
        }

        HttpWebRequest GetRequest(string url, string referer, NameValueCollection requestHeaders)
        {
            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "*/*";
            request.CookieContainer = CookieContainer;
            request.KeepAlive = this.KeepAlive;
            if (this.UserAgent == null)
            {
                request.UserAgent = _userAgentList[Guid.NewGuid().GetHashCode() % _userAgentList.Count];
            }
            if (referer == null && RequestUrlList.Count > 0)
            {
                request.Referer = RequestUrlList[RequestUrlList.Count - 1];
            }
            else
                request.Referer = referer;
            if (this.AllowAutoRedirect.HasValue)
            {
                request.AllowAutoRedirect = this.AllowAutoRedirect.Value;
            }
            if (requestHeaders != null)
            {
                foreach (string item in requestHeaders.Keys)
                {
                    request.Headers[item] = requestHeaders[item];
                }
            }
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
                request.Credentials = CredentialCache.DefaultCredentials;

            }
            return request;

        }

        HttpWebResponse GetResponse(string url, string referer, NameValueCollection requestHeaders)
        {
            return GetRequest(url, referer, requestHeaders).GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="referer">前导页面</param>
        /// <param name="requestHeaders">附加的Http头，可以为NULL,如 var nvc=  new NameValueCollection(); nvc["Test"]="TestValue";</param>
        /// <returns></returns>
        public HttpResult Get(string url, string referer, NameValueCollection requestHeaders)
        {
            HttpResult result = new HttpResult();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)GetRequest(url, referer, requestHeaders).GetResponse();

                using (response)
                {
                    if (response != null) { result.StatusCode = (int)response.StatusCode; result.ResponseHeader = response.Headers; }
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), this.Encode))
                    {
                        result.ResultHtml = reader.ReadToEnd();
                    }

                    response.Close();
                }


            }
            catch
            {
                result.StatusCode = -1;
                result.ResultHtml = string.Empty;
                if (response != null) result.StatusCode = (int)response.StatusCode;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                RequestUrlList.Add(url);
            }

            return result;
        }


        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="referer">前导页面</param>
        /// <param name="postData">要post的数据，<para> var nvc=  new NameValueCollection(); nvc["userName"]="userName";</para></param>
        /// <returns></returns>
        public HttpResult Post(string url, string referer, NameValueCollection postData)
        {
            return Post(url, referer, postData, new NameValueCollection());
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">要post的数据，</param>
        /// <returns></returns>
        public HttpResult Post(string url, NameValueCollection postData)
        {
            return Post(url, null, postData, new NameValueCollection());
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="referer">前导页</param>
        /// <param name="postData">要post的数据</param>
        /// <param name="requestHeaders">附件的http请求头</param>
        /// <returns></returns>
        public HttpResult Post(string url, string referer, NameValueCollection postData, NameValueCollection requestHeaders)
        {
            string postStr = null;
            if (!(postData == null || postData.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                foreach (string key in postData.Keys)
                {
                    buffer.AppendFormat("&{0}={1}", key, TxtEncode(postData[key]));
                }
                postStr = buffer.ToString().Remove(0, 1);
            }
            return Post(url, referer, postStr, requestHeaders);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="referer">前导页面</param>
        /// <param name="postData">String 类型的,Post的数据</param>
        /// <param name="requestHeaders">附件的http头</param>
        /// <returns></returns>
        public HttpResult Post(string url, string referer, string postData, NameValueCollection requestHeaders)
        {
            HttpResult result = new HttpResult();
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {

                request = GetRequest(url, referer, requestHeaders);
                request.Method = "POST";
                request.Accept = "*/*";
                request.CookieContainer = CookieContainer;
                request.ContentType = "application/x-www-form-urlencoded; charset=" + this.Encode.WebName;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = this.KeepAlive;
                if (this.UserAgent == null)
                {
                    request.UserAgent = _userAgentList[Guid.NewGuid().GetHashCode() % _userAgentList.Count];
                }
                if (referer == null && RequestUrlList.Count > 0)
                {
                    request.Referer = RequestUrlList[RequestUrlList.Count - 1];
                }
                if (this.AllowAutoRedirect.HasValue && this.AllowAutoRedirect.Value)
                {
                    request.AllowAutoRedirect = true;
                }
                if (requestHeaders != null)
                {
                    foreach (string item in requestHeaders.Keys)
                    {
                        request.Headers[item] = requestHeaders[item];
                    }
                }
                if (postData != null && postData.Length > 0)
                {
                    byte[] b = this.Encode.GetBytes(postData);
                    request.ContentLength = b.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(b, 0, b.Length);
                    }
                }


                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
                    request.Credentials = CredentialCache.DefaultCredentials;

                }

                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response != null) { result.StatusCode = (int)response.StatusCode; result.ResponseHeader = response.Headers; }
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), this.Encode))
                    {
                        result.ResultHtml = reader.ReadToEnd();
                    }

                    response.Close();
                }


            }
            catch
            {
                result.StatusCode = -1;
                result.ResultHtml = string.Empty;
                if (response != null) result.StatusCode = (int)response.StatusCode;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                RequestUrlList.Add(url);
            }

            return result;
        }

        public static T Post<T>(string url, string postData) where T : class
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(postData);//把字符串转换为字节

            req.ContentLength = data.Length; //请求长度

            using (Stream reqStream = req.GetRequestStream()) //获取
            {
                reqStream.Write(data, 0, data.Length);//向当前流中写入字节
                reqStream.Close(); //关闭当前流
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static T Post<T>(string url, string postData, NameValueCollection requestHeaders) where T : class
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(postData);//把字符串转换为字节

            req.ContentLength = data.Length; //请求长度

            if (requestHeaders != null)
            {
                foreach (string item in requestHeaders.Keys)
                {
                    req.Headers[item] = requestHeaders[item];
                }
            }

            using (Stream reqStream = req.GetRequestStream()) //获取
            {
                reqStream.Write(data, 0, data.Length);//向当前流中写入字节
                reqStream.Close(); //关闭当前流
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        public HttpResult PostData(string url, NameValueCollection nvc, string referUrl)
        {
            HttpResult result = new HttpResult();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = GetRequest(url, null, nvc);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                result.StatusCode = (int)wresp.StatusCode;
                using (StreamReader reader = new StreamReader(stream2, this.Encode))
                {
                    result.ResultHtml = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (wresp != null)
                {
                    result.StatusCode = (int)wresp.StatusCode;

                    wresp.Close();
                    wresp = null;
                }
                else
                {
                    result.StatusCode = -1;
                }
                return result;
            }
            finally
            {
                if (wr != null) wr.Abort();
                if (wresp != null)
                {
                    wresp.Close(); wresp = null;
                }

            }
        }

        /// <summary>
        /// Post文件，如果表单有Input File,则使用此方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="filePath">文件地址</param>
        /// <param name="paramName">input file表单控件的name属性</param>
        /// <param name="contentType">Content-Type属性，如rar为: application/octet-stream</param>
        /// <param name="nvc">附件的表单数据</param>
        public HttpResult PostFile(string url, string filePath, string paramName, string contentType, NameValueCollection nvc)
        {
            string fileName = string.Empty;
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
                fileName = "";
            else
                fileName = file.Name;

            HttpResult result = new HttpResult();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = GetRequest(url, null, new NameValueCollection());
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = this.Encode.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);


            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, fileName, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            if (!string.IsNullOrEmpty(fileName))
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    rs.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                result.StatusCode = (int)wresp.StatusCode;
                using (StreamReader reader = new StreamReader(stream2, this.Encode))
                {
                    result.ResultHtml = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (wresp != null)
                {
                    result.StatusCode = (int)wresp.StatusCode;

                    wresp.Close();
                    wresp = null;
                }
                else
                {
                    result.StatusCode = -1;
                }
                return result;
            }
            finally
            {
                if (wr != null) wr.Abort();
                if (wresp != null)
                {
                    wresp.Close(); wresp = null;
                }

            }
        }

        /// <summary>
        /// Post文件，如果表单有Input File,则使用此方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="filePath">文件地址</param>
        /// <param name="paramName">input file表单控件的name属性</param>
        /// <param name="contentType">Content-Type属性，如rar为: application/octet-stream</param>
        /// <param name="nvc">附件的表单数据</param>
        public HttpResult PostFile(string url, string filePath, string paramName, string contentType, string referUrl, NameValueCollection nvc)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                throw new FileNotFoundException();
            }

            HttpResult result = new HttpResult();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = GetRequest(url, referUrl, new NameValueCollection());
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);


            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file.Name, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                result.StatusCode = (int)wresp.StatusCode;
                using (StreamReader reader = new StreamReader(stream2, this.Encode))
                {
                    result.ResultHtml = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (wresp != null)
                {
                    result.StatusCode = (int)wresp.StatusCode;

                    wresp.Close();
                    wresp = null;
                }
                else
                {
                    result.StatusCode = -1;
                }
                return result;
            }
            finally
            {
                if (wr != null) wr.Abort();
                if (wresp != null)
                {
                    wresp.Close(); wresp = null;
                }

            }
        }

        /// <summary>
        /// 根据地址获取图片，多用于验证码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        //public MediaTypeNames.Image GetImage(string url)
        //{

        //    Bitmap image = null;
        //    using (var response = (HttpWebResponse)GetRequest(url, null, null).GetResponse())
        //    {
        //        image = new System.Drawing.Bitmap(response.GetResponseStream());
        //        response.Close();
        //    }
        //    return image;

        //}

        public string TxtEncode(string str)
        {
            if (str == null) return "";
            byte[] byteStr = this.Encode.GetBytes(str);
            StringBuilder sb = new StringBuilder(byteStr.Length * 3);
            for (int i = 0; i < byteStr.Length; i++)
            {
                sb.AppendFormat(@"%{0}", Convert.ToString(byteStr[i], 16));
            }
            return sb.ToString();
        }


        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postParam">请求参数</param>
        /// <param name="contentType"></param>
        /// <returns>流信息</returns>
        public static string DoGetSSL(string url, string getParam, string requestType = "GET", string encode = "utf-8", int timeout = 100000, string authorization = "")
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                if (!string.IsNullOrEmpty(getParam))
                {
                    url = url + "?" + getParam;
                }
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

                req.Method = requestType;
                req.KeepAlive = false;
                req.Timeout = timeout;
                if (!string.IsNullOrEmpty(authorization))
                {
                    req.Headers.Add("Authorization", authorization);
                }
                using (var response = (HttpWebResponse)req.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    if (stream == null)
                        return string.Empty;
                    var reader = new StreamReader(stream);
                    var data = reader.ReadToEnd();
                    return data;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.ToString());
            }
            return "";
        }


        public static string DoPostSSL(string url, string postParam, string encode = "utf-8", string contentType = "application/x-www-form-urlencoded", int timeout = 100000, string authorization = "")
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                var param = Encoding.GetEncoding(encode).GetBytes(postParam);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

                req.Method = "POST";
                req.ContentType = contentType;
                req.ContentLength = param.Length;
                req.KeepAlive = false;
                req.Timeout = timeout;
                if (!string.IsNullOrEmpty(authorization))
                {
                    req.Headers.Add("Authorization", authorization);
                }
                if (param.Length > 0)
                {
                    using (var reqstream = req.GetRequestStream())
                    {
                        reqstream.Write(param, 0, param.Length);
                    }
                }
                using (var response = (HttpWebResponse)req.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    if (stream == null)
                        return string.Empty;
                    var reader = new StreamReader(stream);
                    var data = reader.ReadToEnd();
                    return data;
                }
            }
            catch (WebException ex)
            {
                var resp = (HttpWebResponse)ex.Response;
                var stream = resp.GetResponseStream();
                if (stream == null)
                    return string.Empty;
                var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                // LogHelper.Error(ex.ToString());
            }
            return "";
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受  
            return true;
        }
    }
}
