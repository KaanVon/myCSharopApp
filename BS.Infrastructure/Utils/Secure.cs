using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BS.Infrastructure.Utils
{
    /// 安全工具类
    /// </summary>
    public sealed class Secure
    {
        #region MD5加密
        /// <summary>
        /// 字符串按MD5进行加密
        /// </summary>
        public static string EncryptStringByMd5(string strP)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(strP);
            //将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;

        }
        #endregion

        #region DES加密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="pToEncrypt">需要加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string EncryptStringByDES(string pToEncrypt)
        {
            string sKey = DES_Key;

            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            ////把字符串放到byte数组中
            //byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            ////建立加密对象的密钥和偏移量
            ////使得输入密码必须输入英文文本
            //des.Key = Encoding.ASCII.GetBytes(sKey);
            //des.IV = Encoding.ASCII.GetBytes(sKey);
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            //cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();
            //StringBuilder ret = new StringBuilder();
            //foreach (byte b in ms.ToArray())
            //{
            //    ret.AppendFormat("{0:X2}", b);
            //}
            //return ret.ToString();

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion

        #region DES解密
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string DecryptStringByDES(string pToDecrypt)
        {
            string sKey = DES_Key;
            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            //for (int x = 0; x < pToDecrypt.Length / 2; x++)
            //{
            //    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            //    inputByteArray[x] = (byte)i;
            //}
            //des.Key = Encoding.ASCII.GetBytes(sKey);
            //des.IV = Encoding.ASCII.GetBytes(sKey);
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();
            ////建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
            //StringBuilder ret = new StringBuilder();
            //return System.Text.Encoding.Default.GetString(ms.ToArray());
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }

        }
        #endregion

        private static string _Des_Key = "12345678";
        #region DES加密Key
        /// <summary>
        /// 8位有效DES加密Key
        /// </summary>
        public static string DES_Key
        {
            get { return _Des_Key; }
            set { _Des_Key = value; }
        }

        #endregion

        #region 一种加密方式
        public static string MixEncrypt(string value, string salt)
        {
            string p = EncryptStringByMd5(value) + EncryptStringByMd5(salt);
            p = EncryptStringByDES(p);
            return p;
        }
        #endregion

        #region 加密盐
        public static string CreateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }
        #endregion
    }
}
