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
    /// 权限
    /// </summary>
    [Table("A_Permission")]
    public class Permission : BaseEntity
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
    }
}
