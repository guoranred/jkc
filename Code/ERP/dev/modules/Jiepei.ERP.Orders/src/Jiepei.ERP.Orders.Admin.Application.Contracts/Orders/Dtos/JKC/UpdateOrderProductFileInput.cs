namespace Jiepei.ERP.Orders.Admin
{
    public class UpdateOrderProductFileInput
    {
        public UpdateOrderProductFileInput()
        {

        }

        public UpdateOrderProductFileInput(string fileName, string filePath)
        {
            this.FileName = fileName;
            this.FilePath = filePath;
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
}
