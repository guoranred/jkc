using Volo.Abp.Threading;

namespace Jiepei.ERP.Orders
{
    public static class OrdersDtoExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            //OneTimeRunner.Run(() =>
            //{
            //    /* You can add extension properties to DTOs
            //     * defined in the depended modules.
            //     *
            //     * Example:
            //     *
            //     * ObjectExtensionManager.Instance
            //     *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
            //     *
            //     * See the documentation for more:
            //     * https://docs.abp.io/en/abp/latest/Object-Extensions
            //     */

            //    ObjectExtensionManager.Instance
            //    .AddOrUpdateProperty<CreateOrderExtraDto, string>("proName", options =>
            //    {
            //        options.Attributes.Add(new RequiredAttribute());
            //        options.Attributes.Add(new MaxLengthAttribute(50));
            //    })
            //    .AddOrUpdateProperty<CreateOrderExtraDto, string>("picture", options =>
            //    {
            //        options.Attributes.Add(new MaxLengthAttribute(200));
            //    })
            //    .AddOrUpdateProperty<CreateOrderExtraDto, string>("fileName", options =>
            //    {
            //        options.Attributes.Add(new MaxLengthAttribute(100));
            //    })
            //    .AddOrUpdateProperty<CreateOrderExtraDto, string>("filePath", options =>
            //    {
            //        options.Attributes.Add(new MaxLengthAttribute(300));
            //    })
            //    /* 框架会为枚举自动添加 RequiredAttribute 和 EnumDataTypeAttribute ；
            //     * 
            //     * 更多信息，请查看文档：
            //     * https://docs.abp.io/en/abp/latest/Object-Extensions#default-validation-attributes
            //     */
            //    .AddOrUpdateProperty<CreateOrderExtraDto, EnumMoldMaterial>("material")
            //    .AddOrUpdateProperty<CreateOrderExtraDto, EnumMoldSurface>("surface");
            //});
        }
    }
}
