using BS.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Entity.Sys
{
    [Table("A_User")]
    public class User : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
       
        public string Salt { get; set; }
        public string mobile { get; set; }
        /// <summary>
        /// 1、男；2、女
        /// </summary>
        public int gender { get; set; }
        public string Avatar { get; set; }
        /// <summary>
        /// 超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new  List<UserRole>();
        }
    }
}
