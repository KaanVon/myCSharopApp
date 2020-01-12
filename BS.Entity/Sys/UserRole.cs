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
    /// 用户角色
    /// </summary>
    [Table("A_UserRole")]
    public class UserRole : BaseEntity
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
