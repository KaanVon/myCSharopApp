using BS.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Entity.Sys
{
    /// <summary>
    /// 错误异常表
    /// </summary>
    [Table("A_Error")]
    public class Error : BaseEntity
    {
        /// <summary>
        /// 获取描述当前异常的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 获取调用堆栈上直接帧的字符串表示形式
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 获取描述当前异常的消息 子异常
        /// </summary>
        public string InnerMessage { get; set; }
        /// <summary>
        /// 获取调用堆栈上直接帧的字符串表示形式 子异常
        /// </summary>
        public string InnerStackTrace { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Action { get; set; }
    }
}
