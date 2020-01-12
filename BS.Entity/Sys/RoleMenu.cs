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
    /// 角色菜单
    /// </summary>
    [Table("A_RoleMenu")]
    public class RoleMenu : BaseEntity
    {
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
