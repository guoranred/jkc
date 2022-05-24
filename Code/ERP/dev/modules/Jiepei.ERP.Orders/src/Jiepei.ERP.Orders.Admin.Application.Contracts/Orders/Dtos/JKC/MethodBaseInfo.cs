
namespace Jiepei.ERP.Orders.Admin
{
    public class MethodBaseInfo
    {
        /// <summary>
        /// 方法的命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Method { get; set; }

        public MethodBaseInfo()
        {

        }

        public MethodBaseInfo(string method, string nameSpace)
        {
            NameSpace = nameSpace;
            Method = method;
        }
    }

}
