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
    /// 菜单
    /// </summary>
    [Table("A_Menu")]
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
        public virtual Menu Parent { get; set; }
        public virtual List<Menu> Menus { get; set; }

        public virtual List<Permission> Permissions { get; set; }
        public int Order { get; set; }
        public Menu()
        {
            Menus = new List<Menu>();
            Permissions = new List<Permission>();
        }
    }
}
