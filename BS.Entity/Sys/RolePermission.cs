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
    /// 角色权限
    /// </summary>
    [Table("A_RolePermission")]
    public class RolePermission : BaseEntity
    {
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
