using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Infrastructure.Utils
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public sealed class Log
    {
        #region Write
        /// <summary>
        /// Write
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="messageContent">内容</param>
        public static void Write(string fileName, string messageContent)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string logpath = AppDomain.CurrentDomain.BaseDirectory + @"App_Log\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
                    if (!Directory.Exists(logpath))
                    {
                        Directory.CreateDirectory(logpath);
                    }
                    StreamWriter writer = new StreamWriter(new FileStream(logpath + fileName + ".log", FileMode.Append, FileAccess.Write), System.Text.Encoding.GetEncoding("GBK"));
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + messageContent);
                    writer.Close();
                    break;
                }
                catch
                {
                    // nothing
                }
            }
        }

        #endregion
    }
}
