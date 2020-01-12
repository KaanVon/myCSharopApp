namespace BS.Data
{
    using BS.Entity.Sys;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelEntity : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“ModelEntity”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“BS.Data.ModelEntity”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“ModelEntity”
        //连接字符串。
        public ModelEntity()
            : base("name=ModelEntity")
        {
        }

        static ModelEntity()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ModelEntity>());
        }

        public IDbSet<User> User { get; set; }
        public IDbSet<Role> Role { get; set; }
        public IDbSet<UserRole> UserRole { get; set; }
        public IDbSet<Menu> Menu { get; set; }
        public IDbSet<Permission> Permission { get; set; }
        public IDbSet<RoleMenu> RoleMenu { get; set; }
        public IDbSet<RolePermission> RolePermission { get; set; }


    }


}