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
    /// 角色
    /// </summary>
    [Table("A_Role")]
    public class Role : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        public virtual List<RoleMenu> RoleMenus { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }

        public Role()
        {
            RoleMenus = new List<RoleMenu>();
            RolePermissions = new List<RolePermission>();
            UserRoles = new List<UserRole>();
        }
    }
}
