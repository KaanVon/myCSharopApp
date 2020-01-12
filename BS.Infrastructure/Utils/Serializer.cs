using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BS.Infrastructure.Utils
{
    /// <summary>
    /// 序列化
    /// </summary>
    public sealed class Serializer
    {
        #region XML序列化
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T">将要序列化的 classType</typeparam>
        /// <param name="o">将要序列化的 System.Object</param>
        /// <param name="filePath">文件路径</param>
        public static void XmlSerialize<T>(T o, string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                StreamWriter sw = new StreamWriter(filePath, false);
                formatter.Serialize(sw, o);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region XML反序列化
        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">将要反序列化的 classType</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <returns>将要反序列化的 classType</returns>
        public static T XmlDeSerialize<T>(string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                StreamReader sr = new StreamReader(filePath);
                T o = (T)formatter.Deserialize(sr);
                sr.Close();
                return o;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
