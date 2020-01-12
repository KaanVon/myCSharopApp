using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Infrastructure.Entity
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键 自增长
        /// </summary>
        [Key]
        public int OID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 软删除
        /// </summary>
        public bool GcRecord { get; set; }

        public BaseEntity()
        {
            CreateTime = DateTime.Now;
        }
    }
}
