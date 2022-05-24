using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Jiepei.ERP.Utilities.Pays
{
    /// <summary>
    /// 安徽金密金属支付--现阶段不考虑通用
    /// </summary>
    public class WeChatPayAHOption
    {
        private string certificate;

        private string certificatePassword;

        internal X509Certificate2 Certificate2;

        internal RSA CertificateRSAPrivateKey;

        internal string CertificateSerialNo;

        /// <summary>
        /// 应用号 公众号、移动应用、小程序AppId、企业微信CorpId。
        /// </summary>
        public string AppId
        {
            get;
            set;
        }

        /// <summary>
        /// 应用密钥 企业微信AppSecret，目前仅调用"企业红包API"时使用。
        /// </summary>
        public string AppSecret
        {
            get;
            set;
        }

        /// <summary>
        /// 商户号、服务商户号
        /// </summary>
        public string MchId
        {
            get;
            set;
        }

        /// <summary>
        /// 子商户应用号 目前仅调用服务商API时使用，子商户的公众号、移动应用AppId。
        /// </summary>
        public string SubAppId
        {
            get;
            set;
        }

        /// <summary>
        /// 子商户号 目前仅调用服务商API时使用，子商户的商户号。
        /// </summary>
        public string SubMchId
        {
            get;
            set;
        }


        /// <summary>
        /// API证书(.p12格式) 可为 .p12证书文件路径、.p12证书文件的Base64编码
        /// </summary>
        public string Certificate
        {
            get
            {
                return certificate;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    certificate = value;
                    GetCertificateInfo();
                }
            }
        }

        /// <summary>
        /// API证书密码 默认为商户号
        /// </summary>
        public string CertificatePassword
        {
            get
            {
                if (!string.IsNullOrEmpty(certificatePassword))
                {
                    return certificatePassword;
                }

                return MchId;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    certificatePassword = value;
                    GetCertificateInfo();
                }
            }
        }

        /// <summary>
        /// API密钥
        /// </summary>
        public string APIKey
        {
            get;
            set;
        }

        /// <summary>
        /// APIv3密钥
        /// </summary>
        public string APIv3Key
        {
            get;
            set;
        }

        /// <summary>
        /// RSA公钥 目前仅调用"企业付款到银行卡API"时使用，执行"获取RSA加密公钥API"即可获取。
        /// </summary>
        public string RsaPublicKey
        {
            get;
            set;
        }

        public string NotifyUrl { get; set; }

        private void GetCertificateInfo()
        {
            if (string.IsNullOrEmpty(Certificate) || string.IsNullOrEmpty(CertificatePassword))
            {
                return;
            }

            try
            {
                if (File.Exists(Certificate))
                {
                    Certificate2 = new X509Certificate2(Certificate, CertificatePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
                }
                else
                {
                    Certificate2 = new X509Certificate2(Convert.FromBase64String(Certificate), CertificatePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
                }
            }
            catch (CryptographicException ex)
            {
                throw new Exception("反序列化证书失败，请确认是否为微信支付签发的有效PKCS#12格式证书。原始异常信息：" + ex.Message);
            }

            CertificateSerialNo = Certificate2.GetSerialNumberString();
            CertificateRSAPrivateKey = Certificate2.GetRSAPrivateKey();
        }
    }
}
