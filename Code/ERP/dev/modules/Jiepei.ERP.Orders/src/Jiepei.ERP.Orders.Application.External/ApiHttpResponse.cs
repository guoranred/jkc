namespace Jiepei.ERP.Orders.Application.External
{
    public class ApiHttpResponse
    {
        private ApiHttpResponse(bool status, string message, object data = null, string code = null)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
            this.Code = code;
        }

        public ApiHttpResponse(bool status, string message) : this(status, message, null, null)
        {

        }

        public ApiHttpResponse(bool status, string message, object data) : this(status, message, data, null)
        {

        }

        public ApiHttpResponse(string message) : this(false, message)
        {

        }

        public ApiHttpResponse()
        {

        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
