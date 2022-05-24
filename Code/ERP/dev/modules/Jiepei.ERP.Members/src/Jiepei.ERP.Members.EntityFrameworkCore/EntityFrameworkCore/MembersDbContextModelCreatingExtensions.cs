using Jiepei.ERP.CodeGenerations;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    public static class MembersDbContextModelCreatingExtensions
    {
        public static void ConfigureMembers(
            this ModelBuilder builder,
            Action<MembersModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MembersModelBuilderConfigurationOptions(
                MembersDbProperties.DbTablePrefix,
                MembersDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<CustomerService>(entity =>
            {
                entity.HasComment("客服表");

                entity.ToTable("CustomerService");

                entity.ConfigureByConvention();

                entity.Property(o => o.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(80)
                    .HasComment("客服名称");

                entity.Property(o => o.Phone)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(20)
                    .HasComment("客服手机号");

                entity.Property(o => o.AvatarImage)
                    .IsUnicode(false)
                    .HasMaxLength(300)
                    .HasComment("客服头像图片地址");

                entity.Property(o => o.WeChatImage)
                    .IsUnicode(false)
                    .HasMaxLength(300)
                    .HasComment("客服微信名片图片地址");

                entity.Property(o => o.QQ)
                    .IsUnicode(false)
                    .HasMaxLength(20)
                    .HasComment("客服QQ号");

                entity.Property(o => o.Email)
                    .IsUnicode(false)
                    .HasMaxLength(50)
                    .HasComment("客服邮箱");

                entity.Property(o => o.Type)
                    .HasComment("客服类型 0客服 1 业务员 2 渠道业务员");

                entity.Property(o => o.PromoCode)
                  .IsUnicode(false)
                    .HasMaxLength(100)
                     .HasComment("业务员推广码");

                entity.Property(o => o.BusinessLine)
                .HasColumnType("varchar(32)")
                .HasComment("业务线");

                entity.Property(o => o.JobNumber)
                 .HasColumnType("varchar(10)")
                 .HasComment("工号");

                entity.Property(o => o.IsOnline)
                .HasComment("是否在线");

            });

            builder.Entity<MemberInformation>(m =>
            {
                m.HasComment("客户表");

                m.ToTable("MemberInformation");

                m.ConfigureByConvention();

                #region 注释
                m.Property(o => o.Code).IsRequired().HasMaxLength(30).HasComment("会员编号");
                m.Property(o => o.Name).HasMaxLength(30).HasComment("会员名称");
                m.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(11).HasComment("手机号码");
                m.Property(o => o.Password).IsRequired().HasMaxLength(200).HasComment("密码");
                m.Property(o => o.Gender).HasComment("性别");
                m.Property(o => o.QQ).HasMaxLength(30).HasComment("QQ");
                m.Property(o => o.CompanyName).HasMaxLength(100).HasComment("公司名称");
                m.Property(o => o.Email).IsUnicode(false).HasMaxLength(50).HasComment("邮件地址");
                m.Property(o => o.CompanyTypeCode).HasMaxLength(50).HasComment("公司类型Code");
                m.Property(o => o.CompanyTypeName).HasMaxLength(50).HasComment("公司类型名称");
                m.Property(o => o.MainProductCode).HasMaxLength(30).HasComment("主营产品Code");
                m.Property(o => o.MainProductName).HasMaxLength(30).HasComment("主营产品名称");
                m.Property(o => o.Industry).HasMaxLength(100).HasComment("所属行业");
                m.Property(o => o.ProfessionCode).HasMaxLength(30).HasComment("职业属性Code");
                m.Property(o => o.ProfessionName).HasMaxLength(30).HasComment("职业属性名称");
                m.Property(o => o.PromoCode).HasMaxLength(30).HasComment("业务员推广码(选填)");
                m.Property(o => o.ProfilePhotoUrl).HasMaxLength(200).HasComment("头像地址");
                m.Property(o => o.ProvinceCode).HasMaxLength(30).HasComment("省code");
                m.Property(o => o.ProvinceName).HasMaxLength(30).HasComment("省名称");
                m.Property(o => o.CityCode).HasMaxLength(30).HasComment("市code");
                m.Property(o => o.CityName).HasMaxLength(30).HasComment("市名称");
                m.Property(o => o.CustomerServiceId).HasComment("客服Id");
                #endregion

                #region 索引
                m.HasIndex(x => x.PhoneNumber, "Index_MemberInformation_PhoneNumber");
                m.HasIndex(x => x.Password, "Index_MemberInformation_Password");
                #endregion
            });

            builder.Entity<MemberAddress>(m =>
            {
                m.HasComment("客户地址表");

                m.ToTable("MemberAddress");

                m.ConfigureByConvention();

                #region 注释
                m.Property(o => o.MemberId).IsRequired().HasComment("关联会员ID");
                m.Property(o => o.Recipient).IsRequired().HasMaxLength(30).HasComment("收货人");
                m.Property(o => o.CompanyName).HasMaxLength(100).HasComment("公司名称");
                m.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(11).HasComment("手机号码");
                m.Property(o => o.ProvinceCode).IsRequired().HasMaxLength(30).HasComment("省code");
                m.Property(o => o.ProvinceName).IsRequired().HasMaxLength(30).HasComment("省Name");
                m.Property(o => o.CityCode).IsRequired().HasMaxLength(30).HasComment("市code");
                m.Property(o => o.CityName).IsRequired().HasMaxLength(30).HasComment("市Name");
                m.Property(o => o.CountyCode).IsRequired().HasMaxLength(30).HasComment("县区code");
                m.Property(o => o.CountyName).IsRequired().HasMaxLength(30).HasComment("县区Name");
                m.Property(o => o.DetailAddress).IsRequired().HasMaxLength(200).HasComment("详细地址");
                m.Property(o => o.IsDefault).HasComment("是否默认地址");
                #endregion

                #region 索引
                m.HasIndex(x => x.MemberId, "Index_MemberAddress_MemberId");
                #endregion

            });

            builder.Entity<AdministrativeDivision>(b =>
            {
                b.HasComment("行政区域表");
                b.ToTable("AdministrativeDivision");
                b.ConfigureByConvention();

                #region 注释
                b.Property(t => t.Code).HasColumnType("varchar(36)").HasComment("编码");
                b.Property(t => t.Name).HasColumnType("nvarchar(36)").HasComment("地区名称");
                b.Property(t => t.Pid).HasComment("父Id");
                b.Property(t => t.Level).HasComment("地区级别");
                b.Property(t => t.EnglishName).HasColumnType("nvarchar(100)").HasComment("地区英文名称");
                b.Property(t => t.Sort).HasComment("序号");
                b.Property(t => t.RelationNmId).HasComment("关联内贸Id");
                #endregion

                #region 索引
                b.HasIndex(x => x.Code, "Index_AdministrativeDivision_Code");
                #endregion
            });

            builder.Entity<CodeGeneration>(b =>
            {
                b.ToTable("CodeGeneration");
                b.HasComment("生成CODE表");
                b.ConfigureByConvention();
            });
        }
    }
}